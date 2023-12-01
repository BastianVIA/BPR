using Frontend.Entities;
using Frontend.Exceptions;
using Frontend.Model;
using Frontend.Service.AlertService;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Frontend.Pages;

public class ActuatorInfoBase : ComponentBase
{
    [Inject] 
    public IActuatorSearchModel ActuatorModel { get; set; }
    
    [Inject]
    public IAlertService AlertService { get; set; }

    // Radzen needs a class to specify the data object
    public Actuator SearchActuator { get; set; } = new();
    public List<Actuator> actuators = new();

    // Blazor page needs an empty constructor
    public ActuatorInfoBase() { }
    
    // Overloaded constructor for unit testing
    public ActuatorInfoBase(IActuatorSearchModel model)
    {
        ActuatorModel = model;
    }
    public async Task SearchActuators()
    {
        try
        {
            actuators = await ActuatorModel.GetActuatorsByPCBA(SearchActuator.PCBA.PCBAUid);
        }
        catch (NetworkException e)
        {
            AlertService.FireEvent(AlertStyle.Danger, e.Message);
        }
    }
}