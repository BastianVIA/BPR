using Frontend.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class ActuatorTableBase : ComponentBase
{
    [Parameter]
    public List<Actuator> Actuators { get; set; } = new();
    
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
}