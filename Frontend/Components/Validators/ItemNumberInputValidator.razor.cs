using Frontend.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components.Validators;

public class ItemNumberInputValidatorBase : ComponentBase
{
    [Parameter] public Actuator Actuator { get; set; }
}