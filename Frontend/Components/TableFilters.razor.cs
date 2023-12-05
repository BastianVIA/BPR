using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class TableFiltersBase : ComponentBase
{
    [Parameter] public EventCallback<List<string>> OnNewFilter { get; set; }

    private readonly string[] _filters =
    {
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

    protected List<string> Filters = new();
    protected readonly List<string> FilterParams = new();

    protected override Task OnInitializedAsync()
    {
        Filters.AddRange(_filters.Take(3));
        FilterParams.AddRange(_filters);
        OnChange();
        return base.OnInitializedAsync();
    }

    protected void OnChange()
    {
        OnNewFilter.InvokeAsync(Filters);
    }
}