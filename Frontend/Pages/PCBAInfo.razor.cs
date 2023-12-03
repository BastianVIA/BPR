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
    public IActuatorDetailsModel _actuatorDetailsModel { get; set; }
    
    public Actuator actuator = new();
    
    [Inject]
    private IAlertService _alertService { get; set; }

    public  bool searchPerfomed = false;
    public PCBAInfoBase()
    {
    }
    
    public PCBAInfoBase(IActuatorDetailsModel actuatorDetailsModel)
    {
        _actuatorDetailsModel = actuatorDetailsModel;
    }
    
    public async Task SearchActuator()
    {
        searchPerfomed = true;
        try
        {
            actuator = await _actuatorDetailsModel.GetActuatorDetails(actuator.WorkOrderNumber, actuator.SerialNumber);
        }
        catch (NetworkException e)
        {
            Console.WriteLine(e);
            _alertService.FireEvent(AlertStyle.Danger, e.Message);
        }
    }

}