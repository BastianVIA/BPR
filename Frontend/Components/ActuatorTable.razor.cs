using Frontend.Entities;
using Frontend.Model;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class ActuatorTableBase : ComponentBase
{
    [Parameter] public List<Actuator> Actuators { get; set; } = new();

    [Parameter] public EventCallback<Actuator> OnActuatorSelected { get; set; }
    
    [Parameter] public EventCallback<List<string>> OnDownloadSelected { get; set; }
    public List<string> Filters { get; set; } = new();
    
    public void UpdateFilters(List<string>? filters)
    {
        filters ??= new List<string>();
        Filters = filters;
    }

    public bool ShouldShowColumn(string name)
    {
        return Filters.Contains(name);
    }

    public void SelectActuator(Actuator actuator)
    {
        OnActuatorSelected.InvokeAsync(actuator);
    }

    public void DownloadCsv()
    {
        OnDownloadSelected.InvokeAsync(Filters);
    }
}