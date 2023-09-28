using Application.CreateActuator;
using Backend.Model;
using BuildingBlocks.Application;

namespace Backend.Services;
using Microsoft.Extensions.Hosting;

public class LINTestBackgroundService: BackgroundService
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
            
            var filePath = "C:\\Users\\Administrator\\Desktop\\inputFiles\\firstFile.csv";
            var csvModel = CSVHandler.ReadCSV(filePath);
            var command = CreateActuatorCommand.Create(int.Parse(csvModel.WorkOrderNumber),
                int.Parse(csvModel.SerialNumber), int.Parse(csvModel.PCBAUid));
          await  _commandBus.Send(command, stoppingToken);
            
          
           // Console.WriteLine("read from csv file " + csvModel.PCBAUid);
        
            // dataService.SaveData(csvModel);
           // Console.WriteLine("Data saved successfully.");

            await Task.Delay(TimeSpan.FromMinutes(0.1));
        }
       
    }
}