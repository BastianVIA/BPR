using Frontend.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class TestResultTableBase : ComponentBase
{
    [Parameter] public List<TestResult> TestResults { get; set; }
    [Parameter] public EventCallback<TestResult> OnActuatorSelected { get; set; }
    
    protected string[] FilterOptions =
    {
        "Work Order Number",
        "Serial Number",
        "Tester",
        "Bay",
        "Error Count"
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
    
    protected void SelectRow(TestResult testResult)
    {
        OnActuatorSelected.InvokeAsync(testResult);
    }
}