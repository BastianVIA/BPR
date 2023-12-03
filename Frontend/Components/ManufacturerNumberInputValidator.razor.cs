using Frontend.Config;
using Frontend.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class ManufacturerNumberInputValidatorBase : ComponentBase
{
    [Inject] ValidationSettings Validation { get; set; }
    
    [Parameter]
    public Actuator Actuator { get; set; }

    protected string RegexText { get; private set; }
    protected string RegexPattern { get; private set; }

    protected override void OnInitialized()
    {
        var length = Validation .ManufacturerNumberLength;
        RegexText = $"manufacturer number must be {length} digits";
        RegexPattern = $"\\d{{{length}}}";
    }
}