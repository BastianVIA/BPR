using Frontend.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class TestResultDetailsBase : ComponentBase
{
    [Parameter]
    public TestResult TestResult { get; set; }
}