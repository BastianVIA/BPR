using Frontend.Entities;
using Frontend.Model;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public class PCBAInfoBase : ComponentBase
{
    [Inject]
    public IActuatorDetailsModel _actuatorDetailsModel { get; set; }
    
    public Actuator actuator = new();

    
    public PCBAInfoBase()
    {
    }
    
    public PCBAInfoBase(IActuatorDetailsModel actuatorDetailsModel)
    {
        _actuatorDetailsModel = actuatorDetailsModel ?? throw new ArgumentNullException(nameof(actuatorDetailsModel));
    }
    
   
    public async Task SearchActuator()
    {
        if (_actuatorDetailsModel == null) 
            throw new InvalidOperationException("ActuatorDetailsModel has not been initialized.");

        if (actuator == null) 
            throw new InvalidOperationException("Actuator is null.");

        actuator = await _actuatorDetailsModel.GetActuatorDetails(actuator.WorkOrderNumber, actuator.SerialNumber);
    }

}