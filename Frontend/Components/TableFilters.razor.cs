using Frontend.Service;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class TableFiltersBase : ComponentBase
{
    // Faked enums, husk at skifte til den fra endpoint
    [Parameter] public EventCallback<List<CsvProperties>> OnNewFilter { get; set; }
    [Parameter] public List<CsvProperties> FilterOptionsEnums { get; set; }
    [Parameter] public List<CsvProperties> InitFilters { get; set; }
    
    protected List<string> CurrentFilters { get; set; }
    protected List<string> FilterOptions { get; set; }

    private Dictionary<string, CsvProperties> _enumMap = new();
    protected override Task OnInitializedAsync()
    {
        InitEnumMap(FilterOptionsEnums);
        FilterOptions = EnumListToStringList(FilterOptionsEnums);
        CurrentFilters = EnumListToStringList(InitFilters);
        OnChange();
        return base.OnInitializedAsync();
    }

    private void InitEnumMap(List<CsvProperties> enumList)
    {
        _enumMap = enumList.ToDictionary(enumValue => enumValue.ToString().Replace("_", " "));
    }

    private List<string> EnumListToStringList(List<CsvProperties> enumList)
    {
        return enumList.Select(prop => prop.ToString().Replace("_", " ")).ToList();
    }

    private List<CsvProperties> GetSelectedAsListOfEnums()
    {
        return CurrentFilters.Select(key => _enumMap[key]).ToList();
    }

    protected void OnChange()
    {
        OnNewFilter.InvokeAsync(GetSelectedAsListOfEnums());
    }
}