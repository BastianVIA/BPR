using Application.CreateOrUpdateActuator;
using Backend.Model;
using BuildingBlocks.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LINTest.Services;

public class LINTestBackgroundService : BackgroundService
{
    private ICommandBus _commandBus;
    private int runIntervalInSeconds; 
    
    public LINTestBackgroundService(ICommandBus commandBus, IConfiguration configuration)
    {
        _commandBus = commandBus;
        var linTestConfig = configuration.GetSection("LINTest");
        runIntervalInSeconds = linTestConfig.GetValue<int>("RunIntervalInSeconds");
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var filePath = "../LINTest/firstFile.csv"; 
                var csvModel = CSVHandler.ReadCSV(filePath);
                var command = CreateOrUpdateActuatorCommand.Create(int.Parse(csvModel.WorkOrderNumber),
                    int.Parse(csvModel.SerialNumber), int.Parse(csvModel.PCBAUid));
                await _commandBus.Send(command, CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await Task.Delay(TimeSpan.FromSeconds(runIntervalInSeconds));
        }
    }
}