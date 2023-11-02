using Application.CreateActuator;
using Backend.Model;
using BuildingBlocks.Application;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace LINTest.Services
{
    public class LINTestBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private DateTime? _lastProcessedDateTime;
        private readonly string _folderPath = "C:/Users/Administrator/Desktop/CSVLogs"; 
        private readonly string _lastProcessedDateTimePath = "../LINTest/lastTimeForProcessedData.json";
        private readonly DateTime _initialDateTime = new DateTime(2021, 11, 1, 8, 0, 0);

        public LINTestBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            LoadLastProcessedDateTime();
        }

        private void LoadLastProcessedDateTime()
        {
            if (File.Exists(_lastProcessedDateTimePath))
            {
                var jsonData = File.ReadAllText(_lastProcessedDateTimePath);
                _lastProcessedDateTime = JsonConvert.DeserializeObject<DateTime?>(jsonData);
            }
        }

        private void SaveLastProcessedDateTime(DateTime datetime)
        {
            var jsonData = JsonConvert.SerializeObject(datetime, Formatting.Indented);
            File.WriteAllText(_lastProcessedDateTimePath, jsonData);
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DateTime referenceTime = _lastProcessedDateTime.HasValue ? _lastProcessedDateTime.Value : _initialDateTime;

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var commandBus = scope.ServiceProvider.GetRequiredService<ICommandBus>();

                DateTime mostRecentFileDateTime = referenceTime;

                try
                {
                    var allFiles = Directory.GetFiles(_folderPath, "*.csv");

                    foreach (var filePath in allFiles)
                    {
                        var fileDateTime = File.GetCreationTime(filePath);

                        if (fileDateTime > referenceTime)
                        {
                            var csvModel = CSVHandler.ReadCSV(filePath);

                            if (string.IsNullOrEmpty(csvModel.WorkOrderNumber) || 
                                string.IsNullOrEmpty(csvModel.SerialNumber) || 
                                string.IsNullOrEmpty(csvModel.PCBAUid))
                            {
                                Console.WriteLine($"Invalid data in file: {filePath}");
                                continue;
                            }

                            if (!int.TryParse(csvModel.WorkOrderNumber, out int workOrderNumber) || 
                                !int.TryParse(csvModel.SerialNumber, out int serialNumber) || 
                                !int.TryParse(csvModel.PCBAUid, out int pcbaUid))
                            {
                                Console.WriteLine($"Invalid numeric data in file: {filePath}");
                                continue;
                            }

                            var command = CreateActuatorCommand.Create(workOrderNumber, serialNumber, pcbaUid);
                            await commandBus.Send(command, stoppingToken);

                            if (fileDateTime > mostRecentFileDateTime)
                            {
                                mostRecentFileDateTime = fileDateTime;
                            }
                        }
                    }

                    if (mostRecentFileDateTime > referenceTime)
                    {
                        SaveLastProcessedDateTime(mostRecentFileDateTime);
                        _lastProcessedDateTime = mostRecentFileDateTime;
                        referenceTime = mostRecentFileDateTime; 
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                await Task.Delay(TimeSpan.FromMinutes(5));
            }
        }
    }
}
