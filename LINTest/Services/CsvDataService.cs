using BuildingBlocks.Exceptions;
using BuildingBlocks.Integration;
using LINTest.Handlers;
using LINTest.Integration;
using LINTest.Models;
using Microsoft.Extensions.Logging;

namespace LINTest.Services;

public class CsvDataService
{
    private readonly ILogger<CsvDataService> _logger;


    public CsvDataService(ILogger<CsvDataService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task ProcessCsvData(string filePath, IIntegrationEventPublisher publisher,
        CancellationToken cancellationToken)
    {
        var csvModel = CSVHandler.ReadCSV(filePath);
        if (!IsValidCsvData(csvModel, filePath))
        {
            _logger.LogError($"File with path {filePath} does not have valid CSV data");
            return;
        }
        try
        {
            if (csvModel.LINTestPassed)
            {
                await SendTestSucceededEventAsync(csvModel, publisher, cancellationToken);
                foreach (var testError in csvModel.TestErrors)
                {
                    await SendTestFailedEventAsync(testError, publisher, cancellationToken);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError("Error sending event");
            throw new ServiceUnavailableException("Cannot contact service to save test results", e);
        }
    }


    private bool IsValidCsvData(CSVModel csvModel, string filePath)
    {
        if (string.IsNullOrEmpty(csvModel.WorkOrderNumber) ||
            string.IsNullOrEmpty(csvModel.SerialNumber) ||
            string.IsNullOrEmpty(csvModel.PCBAUid))
        {
            _logger.LogWarning("Invalid data in file: {FilePath}", filePath);
            return false;
        }

        if (!int.TryParse(csvModel.WorkOrderNumber, out _) ||
            !int.TryParse(csvModel.SerialNumber, out _) ||
            !int.TryParse(csvModel.PCBAUid, out _))
        {
            _logger.LogWarning("Invalid numeric data in file: {FilePath}", filePath);
            return false;
        }

        return true;
    }

    private async Task SendTestFailedEventAsync(TestErrorModel errorModel, IIntegrationEventPublisher publisher,
        CancellationToken stoppingToken)
    {
        var eventToSend = new ActuatorTestFailedIntegrationEvent(
            errorModel.WorkOrderNumber,
            errorModel.SerialNumber,
            errorModel.Tester,
            errorModel.Bay,
            errorModel.ErrorCode,
            errorModel.ErrorMessage,
            errorModel.TimeOccured);
        await publisher.PublishAsync(eventToSend, stoppingToken);
    }

    private async Task SendTestSucceededEventAsync(CSVModel csvModel, IIntegrationEventPublisher publisher,
        CancellationToken cancellationToken)
    {
        var eventToSend = new ActuatorTestSucceededIntegrationEvent(
            int.Parse(csvModel.WorkOrderNumber),
            int.Parse(csvModel.SerialNumber),
            csvModel.PCBAUid,
            csvModel.ArticleNumber,
            csvModel.ArticleName,
            csvModel.CommunicationProtocol,
            csvModel.CreatedTime,
            csvModel.Tester,
            csvModel.Bay,
            csvModel.MinServoPosition,
            csvModel.MaxServoPosition,
            csvModel.MinBuslinkPosition,
            csvModel.MaxBuslinkPosition,
            csvModel.ServoStroke);
        await publisher.PublishAsync(eventToSend, cancellationToken);
    }
    
}