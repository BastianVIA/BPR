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

    // Blazor page needs an empty constructor
    public ActuatorInfoBase()
    {
        
    }
    
    // Overloaded constructor for unit testing
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
}