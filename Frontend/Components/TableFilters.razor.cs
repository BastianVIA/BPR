using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class TableFiltersBase : ComponentBase
{
    [Parameter] public EventCallback<List<string>> OnNewFilter { get; set; }
    
    [Parameter]
    public string[] FilterOptions { get; set; }

    protected List<string> Filters = new();
    protected readonly List<string> FilterParams = new();

    protected override Task OnInitializedAsync()
    {
        Filters.AddRange(FilterOptions.Take(3));
        FilterParams.AddRange(FilterOptions);
        OnChange();
        return base.OnInitializedAsync();
    }

    protected void OnChange()
    {
        OnNewFilter.InvokeAsync(Filters);
    }
}