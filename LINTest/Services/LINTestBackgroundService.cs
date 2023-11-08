using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BuildingBlocks.Application;

namespace LINTest.Services
{
    public class LINTestBackgroundService : BackgroundService
    {
        private readonly ConfigurationManager _configManager;
        private readonly FileProcessor _fileProcessor;
        private readonly CsvDataService _csvDataService;
        private readonly IServiceProvider _serviceProvider;
        private readonly StateManager _stateManager;


        public LINTestBackgroundService(
            IServiceProvider serviceProvider,
            IConfiguration configuration,
            FileProcessorOptions fileProcessorOptions,
            StateManagerOptions stateManagerOptions)
        {
            _serviceProvider = serviceProvider;
            _configManager = new ConfigurationManager(configuration);
            _fileProcessor = new FileProcessor(fileProcessorOptions);
            _csvDataService = new CsvDataService();
            _stateManager = new StateManager(stateManagerOptions);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DateTime lastProcessedFileTime = _stateManager.LoadLastProcessedDateTime() ?? DateTime.MinValue;

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var commandBus = scope.ServiceProvider.GetRequiredService<ICommandBus>();

                var allFiles = _fileProcessor.GetCsvFiles();

                var filesToProcess = allFiles.Select(filePath => new
                    {
                        Path = filePath,
                        CreationTime = _fileProcessor.GetFileCreationTime(filePath)
                    })
                    .Where(file => file.CreationTime > lastProcessedFileTime)
                    .OrderBy(file => file.CreationTime)
                    .ToList();

                foreach (var file in filesToProcess)
                {
                    try
                    {
                        Console.WriteLine($"Processing file: {file.Path}, created at {file.CreationTime}");
                        await _csvDataService.ProcessCsvData(file.Path, commandBus, stoppingToken);

                        lastProcessedFileTime = file.CreationTime;
                        _stateManager.SaveLastProcessedDateTime(lastProcessedFileTime);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file {file.Path}: {ex.Message}");
                    }
                }

                if (filesToProcess.Any())
                {
                    Console.WriteLine("All new files have been processed.");
                }
                else
                {
                    Console.WriteLine("No new files to process.");
                }

                if (stoppingToken.IsCancellationRequested)
                {
                    Console.WriteLine("Cancellation requested before delay, stopping the service.");
                    return;
                }

                await Task.Delay(TimeSpan.FromSeconds(_configManager.RunIntervalInSeconds), stoppingToken);
            }
        }
        
    }
}