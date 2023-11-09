using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BuildingBlocks.Application;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LINTest.Services
{
    public class LINTestBackgroundService : BackgroundService
    {
        private readonly ConfigurationManager _configManager;
        private readonly FileProcessor _fileProcessor;
        private readonly CsvDataService _csvDataService;
        private readonly IServiceProvider _serviceProvider;
        private readonly FileProcessingStateManager _fileProcessingStateManager;
        private readonly FileProcessorOptions _fileProcessorOptions;
        private readonly StateManagerOptions _stateManagerOptions;
        private readonly ILogger<LINTestBackgroundService> _logger;


        public LINTestBackgroundService(
            IServiceProvider serviceProvider,
            IConfiguration configuration, CsvDataService csvDataService, FileProcessor fileProcessor,
            FileProcessingStateManager fileProcessingStateManager,
            IOptions<FileProcessorOptions> fileProcessorOptions,
            IOptions<StateManagerOptions> stateManagerOptions,ILogger<LINTestBackgroundService> logger)


        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _csvDataService = csvDataService;
            _fileProcessor = fileProcessor;
            _fileProcessingStateManager = fileProcessingStateManager;
            _fileProcessorOptions = fileProcessorOptions.Value;
            _stateManagerOptions = stateManagerOptions.Value;

            _configManager = new ConfigurationManager(configuration);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DateTime lastProcessedFileTime =
                _fileProcessingStateManager.LoadLastProcessedDateTime() ?? DateTime.MinValue;

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
                        _logger.LogInformation($"Processing file: {file.Path}, created at {file.CreationTime}");
                        await _csvDataService.ProcessCsvData(file.Path, commandBus, stoppingToken);

                        lastProcessedFileTime = file.CreationTime;
                        _fileProcessingStateManager.SaveLastProcessedDateTime(lastProcessedFileTime);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error processing file {file.Path}");
                    }
                }

                if (filesToProcess.Any())
                {
                    _logger.LogInformation("All new files have been processed.");
                }
                else
                {
                    _logger.LogInformation("No new files to process.");
                }

                if (stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Cancellation requested before delay, stopping the service.");
                    return;
                }

                await Task.Delay(TimeSpan.FromSeconds(_configManager.RunIntervalInSeconds), stoppingToken);
            }
        }
    }
}