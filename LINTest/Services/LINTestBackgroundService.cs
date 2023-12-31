﻿using BuildingBlocks.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BuildingBlocks.Integration;
using Microsoft.Extensions.Logging;

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

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            DateTime lastProcessedFileTime =
                _fileProcessingStateManager.LoadLastProcessedDateTime() ?? DateTime.MinValue;
            using var scope = _serviceProvider.CreateScope();
            var publisher = scope.ServiceProvider.GetRequiredService<IIntegrationEventPublisher>();

            var namesOfUnprocessedFiles = _fileProcessor.GetCsvFileNamesSince(lastProcessedFileTime);
            
            var numberOfFilesProcessed =
                await ProcessFiles(cancellationToken, namesOfUnprocessedFiles, lastProcessedFileTime, publisher);

            _logger.LogInformation($"{numberOfFilesProcessed} new files have been processed.");

            await Task.Delay(TimeSpan.FromSeconds(_configManager.RunIntervalInSeconds), cancellationToken);
        }

        _logger.LogError("BackgroundService LINTest stopped");
    }

    private async Task<int> ProcessFiles(CancellationToken cancellationToken, string[] allFiles,
        DateTime lastProcessedFileTime,
        IIntegrationEventPublisher publisher)
    {
        var filesToProcess = allFiles.Select(filePath => new
            {
                Path = filePath,
                LastWriteTime = _fileProcessor.GetFileCreationTime(filePath)
            })
            .Where(file => file.LastWriteTime > lastProcessedFileTime)
            .OrderBy(file => file.LastWriteTime)
            .ToList();


        for (var i = 0; i < filesToProcess.Count; i++)
        {
            try
            {
                _logger.LogInformation($"Processing file: {filesToProcess[i].Path}, last editied at {filesToProcess[i].LastWriteTime}");
                await _csvDataService.ProcessCsvData(filesToProcess[i].Path, publisher, cancellationToken);
                
                _fileProcessingStateManager.SaveLastProcessedDateTime(filesToProcess[i].LastWriteTime);
            }
            catch (ServiceUnavailableException e)
            {
                _logger.LogWarning(e, $"Exception while transmitting event about testdata, Will try again next time service runs");
                return i;
            }
            catch (Exception ex)
            {
                _fileProcessingStateManager.ProcessingFailed();
                var consecutiveFails = _fileProcessingStateManager.GetNumberOfConsecutiveFails();
                if (_configManager.MaxConsecutiveFailsBeforeGivingUp > consecutiveFails)
                {
                    _logger.LogWarning(ex,
                        $"Error processing file {filesToProcess[i].Path}. It will be tried again, continuing from this point the next time the service runs");
                    return i;
                }

                _logger.LogError(
                    $"File at {filesToProcess[i].Path} has failed {consecutiveFails} consecutive times, we will therefore skip this file and not retry it. \n " +
                    $"Manuel intervention is needed for this file to be picked up by the system in the future");
            }
        }
        return filesToProcess.Count;
    }
}