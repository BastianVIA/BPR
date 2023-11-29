using Frontend.Entities;
using Frontend.Model;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public class ActuatorInfoBase : ComponentBase
{
    [Inject] 
    public IActuatorSearchModel ActuatorModel { get; set; }
    public string SearchUid { get; set; }
    public List<Actuator> actuators = new();

    public ActuatorInfoBase()
    {
        
    }
    public ActuatorInfoBase(IActuatorSearchModel model)
    {
        ActuatorModel = model;
    }
    public async Task SearchActuators()
    {
        try
        {
            actuators = await ActuatorModel.GetActuatorsByUid(SearchUid);
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
            new Actuator() { SerialNumber = 123, WorkOrderNumber = 6575, PCBA = { PCBAUid = "12345", ManufacturerNumber = 2205}},
            new Actuator() { SerialNumber = 123, WorkOrderNumber = 6575, PCBA = { PCBAUid = "12345", ManufacturerNumber = 6969}},
            new Actuator() { SerialNumber = 123, WorkOrderNumber = 6575, PCBA = { PCBAUid = "12345", ManufacturerNumber = 1420}},
        };
    }
}