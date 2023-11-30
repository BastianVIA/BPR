using Application.CreateOrUpdateActuator;
using Backend.Model;
using BuildingBlocks.Application;
using LINTest.Handlers;
using Microsoft.Extensions.Logging;

namespace LINTest.Services;

public class CsvDataService
{
    private readonly ILogger<CsvDataService> _logger;

    
    public CsvDataService(ILogger<CsvDataService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task ProcessCsvData(string filePath, ICommandBus commandBus, CancellationToken stoppingToken)
    {
        var csvModel = CSVHandler.ReadCSV(filePath); 
        if (IsValidCsvData(csvModel,filePath))
        {
            await SendCommandAsync(csvModel, commandBus, stoppingToken);
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

    private async Task SendCommandAsync(CSVModel csvModel, ICommandBus commandBus, CancellationToken stoppingToken)
    {
        var command = CreateOrUpdateActuatorCommand.Create(
            int.Parse(csvModel.WorkOrderNumber),
            int.Parse(csvModel.SerialNumber),
            csvModel.PCBAUid);
        await commandBus.Send(command, stoppingToken);
    }
}
