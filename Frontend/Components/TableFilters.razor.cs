using Frontend.Service;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class TableFiltersBase : ComponentBase
{
    [Parameter] public EventCallback<List<CsvProperties>> OnNewFilter { get; set; }
    [Parameter] public List<CsvProperties> FilterOptionsEnums { get; set; }
    [Parameter] public List<CsvProperties> InitFilters { get; set; }

    protected List<string> CurrentFilters { get; set; } = new();
    protected List<string> FilterOptions { get; set; } = new();

    private Dictionary<string, CsvProperties> _enumMap = new();
    protected override Task OnInitializedAsync()
    {
        InitEnumMap();
        FilterOptions = EnumListToStringList(FilterOptionsEnums);
        CurrentFilters = EnumListToStringList(InitFilters);
        OnChange();
        return base.OnInitializedAsync();
    }

    private void InitEnumMap()
    {
        _enumMap = FilterOptionsEnums.ToDictionary(EnumToString);
    }

    private List<string> EnumListToStringList(List<CsvProperties> enumList)
    {
        return enumList.Select(EnumToString).ToList();
    }

    private string EnumToString(CsvProperties prop)
    {
        return prop.ToString().Replace("_", " ");
    }

    private List<CsvProperties> GetSelectedAsListOfEnums()
    {
        if (CurrentFilters is null)
        {
            return new List<CsvProperties>();
        }
        return CurrentFilters.Select(key => _enumMap[key]).ToList();
    }

    protected void OnChange()
    {
        OnNewFilter.InvokeAsync(GetSelectedAsListOfEnums());
    }
}