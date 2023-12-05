using Frontend.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class TestResultTableBase : ComponentBase
{
    [Parameter]
    public List<TestResult> TestResults { get; set; }
    protected string[] FilterOptions =
    {
        "Work Order Number",
        "Serial Number",
        "Tester",
        "Bay",
        "Min Servo Position",
        "Max Servo Position",
        "Min Buslink Position",
        "Max Buslink Position",
        "Servo Stroke"
    };
    private List<string> _currentFilters = new();

    protected void UpdateFilters(List<string>? newFilters)
    {
        newFilters ??= new List<string>();
        _currentFilters = newFilters;
    }
    
    protected bool ShouldShowColumn(string name)
    {
        return _currentFilters.Contains(name);
    }
}