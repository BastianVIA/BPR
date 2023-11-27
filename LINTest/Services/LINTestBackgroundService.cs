using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BuildingBlocks.Application;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LINTest.Services;

public class LINTestBackgroundService : BackgroundService
{
    private readonly ConfigurationManager _configManager;
    private readonly FileProcessor _fileProcessor;
    private readonly CsvDataService _csvDataService;
    private readonly IServiceProvider _serviceProvider;
    private readonly FileProcessingStateManager _fileProcessingStateManager;
    private readonly ILogger<LINTestBackgroundService> _logger;

    public LINTestBackgroundService(
        IServiceProvider serviceProvider,
        CsvDataService csvDataService, FileProcessor fileProcessor,
        FileProcessingStateManager fileProcessingStateManager, ILogger<LINTestBackgroundService> logger,
        ConfigurationManager configurationManager)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _csvDataService = csvDataService;
        _fileProcessor = fileProcessor;
        _fileProcessingStateManager = fileProcessingStateManager;
        _configManager = configurationManager;
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

            var numberOfFilesProcessed =
                await ProcessingFiles(stoppingToken, allFiles, lastProcessedFileTime, commandBus);

            if (numberOfFilesProcessed >= 0)
            {
                _logger.LogInformation("All new files have been processed. Count:" + numberOfFilesProcessed);
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

    private async Task<int> ProcessingFiles(CancellationToken stoppingToken, string[] allFiles,
        DateTime lastProcessedFileTime,
        ICommandBus commandBus)
    {
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

        return filesToProcess.Count;
    }
}