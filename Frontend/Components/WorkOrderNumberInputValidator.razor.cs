using Frontend.Config;
using Frontend.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace Frontend.Components;

public class WorkOrderNumberInputValidatorBase : ComponentBase
{
    [Inject] IOptions<ValidationSettings> Validation { get; set; }
    
    [Parameter]
    public Actuator Actuator { get; set; }
    protected string RegexText { get; private set; }
    protected string RegexPattern { get; private set; }
    
    protected override void OnInitialized()
    {
        var length = Validation.Value.WorkOrderNumberLength;
        RegexText = $"WO Number must be {length} digits";
        RegexPattern = $"\\d{{{length}}}";
    }
}