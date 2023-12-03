using Frontend.Entities;
using Frontend.Exceptions;
using Frontend.Model;
using Frontend.Service.AlertService;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Frontend.Pages;

public class PCBAInfoBase : ComponentBase
{
    [Inject]
    public IActuatorDetailsModel ActuatorDetailsModel { get; set; }
    [Inject] 
    private IAlertService _alertService { get; set; }
    
    public Actuator actuator = new();
    public  bool searchPerfomed = false;
    public PCBAInfoBase() { }
    
    public PCBAInfoBase(IActuatorDetailsModel actuatorDetailsModel)
    {
        ActuatorDetailsModel = actuatorDetailsModel;
    }
    
    public async Task SearchActuator()
    {
        searchPerfomed = true;
        try
        {
            // Udkommenteret fordi vi kommer til åbne siden gennem actuator search page
            //actuator = await _actuatorDetailsModel.GetActuatorDetails(actuator.WorkOrderNumber, actuator.SerialNumber);
        }
        catch (NetworkException e)
        {
            Console.WriteLine(e);
            _alertService.FireEvent(AlertStyle.Danger, e.Message);
        }
    }
}