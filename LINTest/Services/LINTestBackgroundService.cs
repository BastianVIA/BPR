using Application.CreateActuator;
using Backend.Model;
using BuildingBlocks.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.IO;

namespace LINTest.Services
{
    public class LINTestBackgroundService : BackgroundService
    {
        private int runIntervalInSeconds;
        private readonly IServiceProvider _serviceProvider;
        private DateTime? _lastProcessedDateTime;
        private readonly string _folderPath = "C:/Users/Administrator/Desktop/CSVLogs";
        private readonly string _processedFolderPath = "C:/Users/Administrator/Desktop/ProcessedCSVLogs";
        private readonly string _lastProcessedDateTimePath = "../LINTest/lastTimeForProcessedData.json";
        private readonly DateTime _initialDateTime = new (2021, 11, 1, 8, 0, 0);

        public LINTestBackgroundService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var linTestConfig = configuration.GetSection("LINTest");
            runIntervalInSeconds = linTestConfig.GetValue<int>("RunIntervalInSeconds");
            _serviceProvider = serviceProvider;
            LoadLastProcessedDateTime();

            // Ensure the processed files directory exists
            Directory.CreateDirectory(_processedFolderPath);
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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
                    mostRecentFileDateTime = await ProcessFiles(allFiles, commandBus, referenceTime, stoppingToken);

                    if (mostRecentFileDateTime > referenceTime)
                    {
                        SaveLastProcessedDateTime(mostRecentFileDateTime);
                        _lastProcessedDateTime = mostRecentFileDateTime;
                        referenceTime = mostRecentFileDateTime;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error processing files: {e.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(runIntervalInSeconds));
            }
        }

        private async Task<DateTime> ProcessFiles(string[] allFiles, ICommandBus commandBus, DateTime referenceTime, CancellationToken stoppingToken)
        {
            DateTime mostRecentFileDateTime = referenceTime;

            foreach (var filePath in allFiles)
            {
                var fileDateTime = File.GetCreationTime(filePath);

                if (fileDateTime > referenceTime)
                {
                    var csvModel = CSVHandler.ReadCSV(filePath);
                    if (IsValidCsvData(csvModel, filePath))
                    {
                        await ProcessCsvData(csvModel, commandBus, stoppingToken, filePath);

                        if (fileDateTime > mostRecentFileDateTime)
                        {
                            mostRecentFileDateTime = fileDateTime;
                        }
                    }
                }
            }

            return mostRecentFileDateTime;
        }

        private bool IsValidCsvData(CSVModel csvModel, string filePath)
        {
            if (string.IsNullOrEmpty(csvModel.WorkOrderNumber) ||
                string.IsNullOrEmpty(csvModel.SerialNumber) ||
                string.IsNullOrEmpty(csvModel.PCBAUid))
            {
                Console.WriteLine($"Invalid data in file: {filePath}");
                return false;
            }

            if (!int.TryParse(csvModel.WorkOrderNumber, out _) ||
                !int.TryParse(csvModel.SerialNumber, out _) ||
                !int.TryParse(csvModel.PCBAUid, out _))
            {
                Console.WriteLine($"Invalid numeric data in file: {filePath}");
                return false;
            }

            return true;
        }

        private async Task ProcessCsvData(CSVModel csvModel, ICommandBus commandBus, CancellationToken stoppingToken, string filePath)
        {
            if (int.TryParse(csvModel.WorkOrderNumber, out int workOrderNumber) &&
                int.TryParse(csvModel.SerialNumber, out int serialNumber) &&
                int.TryParse(csvModel.PCBAUid, out int pcbaUid))
            {
                var command = CreateActuatorCommand.Create(workOrderNumber, serialNumber, pcbaUid);
                await commandBus.Send(command, stoppingToken);
                
            }
        }
    }
}
