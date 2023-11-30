using Backend.Model;
using BuildingBlocks.Integration;
using LINTest.Handlers;
using LINTest.Integration;
using Microsoft.Extensions.Logging;

namespace LINTest.Services;

public class CsvDataService
{
    private readonly ILogger<CsvDataService> _logger;

    
    public CsvDataService(ILogger<CsvDataService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task ProcessCsvData(string filePath, IIntegrationEventPublisher publisher, CancellationToken stoppingToken)
    {
        var csvModel = CSVHandler.ReadCSV(filePath); 
        if (IsValidCsvData(csvModel,filePath))
        {
            await SendCommandAsync(csvModel, publisher, stoppingToken);
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

    private async Task SendCommandAsync(CSVModel csvModel, IIntegrationEventPublisher publisher, CancellationToken stoppingToken)
    {
        var eventToSend = new ActuatorFoundIntegrationEvent(
            int.Parse(csvModel.WorkOrderNumber),
            int.Parse(csvModel.SerialNumber),
            csvModel.PCBAUid);
        await publisher.PublishAsync(eventToSend, stoppingToken);
    }
}
