using Frontend.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public class ActuatorInfoBase : ComponentBase
{

    public List<Actuator> _actuators = new()
    {
        new Actuator() { SerialNumber = 123, WorkOrderNumber = 6575, PCBA = { PCBAUid = 12345}},
        new Actuator() { SerialNumber = 123, WorkOrderNumber = 6575, PCBA = { PCBAUid = 12345}},
        new Actuator() { SerialNumber = 123, WorkOrderNumber = 6575, PCBA = { PCBAUid = 12345}},
    };
}