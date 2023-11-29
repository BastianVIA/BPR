using Frontend.Entities;
using Frontend.Model;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public class ActuatorInfoBase : ComponentBase
{
    [Inject] 
    public IActuatorDetailsModel ActuatorModel { get; set; }
    public string Uid { get; set; }
    public List<Actuator> actuators = new();

    public ActuatorInfoBase()
    {
        
    }
    public ActuatorInfoBase(IActuatorDetailsModel model)
    {
        ActuatorModel = model;
    }
    public async Task SearchPcba()
    {
        try
        {
            actuators = await ActuatorModel.GetActuatorsByUid(Uid);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async Task<List<Actuator>> MockActuators()
    {
        return new()
        {
            new Actuator() { SerialNumber = 123, WorkOrderNumber = 6575, PCBA = { PCBAUid = 12345, ManufacturerNumber = 2205}},
            new Actuator() { SerialNumber = 123, WorkOrderNumber = 6575, PCBA = { PCBAUid = 12345, ManufacturerNumber = 6969}},
            new Actuator() { SerialNumber = 123, WorkOrderNumber = 6575, PCBA = { PCBAUid = 12345, ManufacturerNumber = 1420}},
        };
    }
}