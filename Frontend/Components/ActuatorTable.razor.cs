using Frontend.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class ActuatorTableBase : ComponentBase
{
    [Parameter] public List<Actuator> Actuators { get; set; } = new();

    [Parameter] public EventCallback<Actuator> OnActuatorSelected { get; set; }

    protected string[] FilterOptions { get; } = {
        "Work Order Number",
        "Serial Number",
        "UID",
        "Manufacturer Number",
        "Item Number",
        "Production Date Code",
        "Article Name",
        "Article Number",
        "Communication Protocol",
        "Created Time",
        "Software",
        "Configuration Number"
    };
    private List<string> Filters { get; set; } = new();

    protected void UpdateFilters(List<string>? filters)
    {
        filters ??= new List<string>();
        Filters = filters;
    }

    protected bool ShouldShowColumn(string name)
    {
        return Filters.Contains(name);
    }

    protected void SelectActuator(Actuator actuator)
    {
        OnActuatorSelected.InvokeAsync(actuator);
    }
}