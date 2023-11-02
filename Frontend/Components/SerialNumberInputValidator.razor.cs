using Frontend.Config;
using Frontend.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace Frontend.Components;

public class SerialNumberInputValidatorBase : ComponentBase
{
    [Inject] ValidationSettings Validation { get; set; }
    
    [Parameter]
    public Actuator Actuator { get; set; }

    protected string RegexText { get; private set; }
    protected string RegexPattern { get; private set; }

    protected override void OnInitialized()
    {
        var minLength = Validation.SerialNumberMinLength;
        var maxLength = Validation.SerialNumberMaxLength;

        RegexText = $"Serial Number must be {minLength}-{maxLength} digits";
        RegexPattern = $"(?!0)\\d{{{minLength},{maxLength}}}";
    }
}