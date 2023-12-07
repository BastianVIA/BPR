using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class TestErrorTableFiltersBase : ComponentBase
{
    [Parameter] public EventCallback<List<string>> OnNewFilter { get; set; }

    private readonly string[] _filters =
    {
        "Time Interval",
        "Error Code",
        "Total Errors",
        "Total Tests"
    };

    protected List<string> Filters = new();
    protected readonly List<string> FilterParams = new();

    protected override Task OnInitializedAsync()
    {
        Filters.AddRange(_filters.Take(4));
        FilterParams.AddRange(_filters);
        OnChange();
        return base.OnInitializedAsync();
    }

    protected void OnChange()
    {
        OnNewFilter.InvokeAsync(Filters);
    }
}