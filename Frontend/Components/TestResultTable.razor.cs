using Frontend.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class TestResultTableBase : ComponentBase
{
    [Parameter] public List<TestResult> TestResults { get; set; }
    [Parameter] public EventCallback<TestResult> OnActuatorSelected { get; set; }
}