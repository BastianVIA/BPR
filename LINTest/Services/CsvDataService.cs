using Application.CreateActuator;
using Backend.Model;
using BuildingBlocks.Application;

namespace LINTest.Services;

public class CsvDataService
{
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

    private async Task SendCommandAsync(CSVModel csvModel, ICommandBus commandBus, CancellationToken stoppingToken)
    {
        var command = CreateActuatorCommand.Create(
            int.Parse(csvModel.WorkOrderNumber),
            int.Parse(csvModel.SerialNumber),
            int.Parse(csvModel.PCBAUid));
        await commandBus.Send(command, stoppingToken);
    }
}
