using Application.CreateOrUpdateActuator;
using Backend.Model;
using BuildingBlocks.Application;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using LINTest.Handlers;

namespace LINTest.Services;

public class LINTestBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public LINTestBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();

            var commandBus = scope.ServiceProvider.GetRequiredService<ICommandBus>();
            try
            {
                var filePath = "../LINTest/firstFile.csv";
                var csvModel = CSVHandler.ReadCSV(filePath);
                var command = CreateOrUpdateActuatorCommand.Create(int.Parse(csvModel.WorkOrderNumber),
                    int.Parse(csvModel.SerialNumber), csvModel.PCBAUid);
                await commandBus.Send(command, stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await Task.Delay(TimeSpan.FromSeconds(700));
        }
    }
}