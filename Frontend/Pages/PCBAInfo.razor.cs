using Frontend.Entities;
using Frontend.Model;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public class PCBAInfoBase : ComponentBase
{
    [Inject] private IActuatorDetailsModel ActuatorDetailsModel { get; set; }
    protected int pcbaUid;
    protected Actuator actuator = new();

    protected async Task SearchActuator()
    {
        actuator = await ActuatorDetailsModel.GetActuatorDetails(actuator.WorkOrderNumber, actuator.SerialNumber);
        pcbaUid = actuator.PCBA.PCBAUid;
    }
}