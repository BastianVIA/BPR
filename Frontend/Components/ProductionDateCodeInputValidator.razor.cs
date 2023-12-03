using Frontend.Config;
using Frontend.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class ProductionDateCodeInputValidatorBase : ComponentBase
{
    [Inject] ValidationSettings Validation { get; set; }

    [Parameter] public Actuator Actuator { get; set; }

    protected string RegexText { get; private set; }
    protected string RegexPattern { get; private set; }

    protected override void OnInitialized()
    {
        var minLength = Validation.ProductionDateCodeMinLenght;
        var maxLength = Validation.ProductionDateCodeMaxLenght;

        RegexText = $"Production Date Code must be {minLength}-{maxLength} digits";
        RegexPattern = $"(?!0)\\d{{{minLength},{maxLength}}}";
    }
}