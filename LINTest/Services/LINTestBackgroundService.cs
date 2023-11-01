using Application.CreateActuator;
using Backend.Model;
using BuildingBlocks.Application;
using Microsoft.Extensions.Hosting;

namespace LINTest.Services;

public class LINTestBackgroundService : BackgroundService
{
    private ICommandBus _commandBus;
    
    public LINTestBackgroundService(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var filePath = "../LINTest/firstFile.csv"; 
                var csvModel = CSVHandler.ReadCSV(filePath);
                var command = CreateActuatorCommand.Create(int.Parse(csvModel.WorkOrderNumber),
                    int.Parse(csvModel.SerialNumber), int.Parse(csvModel.PCBAUid));
                await _commandBus.Send(command, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await Task.Delay(TimeSpan.FromMinutes(10));
        }
    }
}