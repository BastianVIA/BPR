using Frontend.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class ItemNumberInputValidatorBase : ComponentBase
{
    [Parameter] public Actuator Actuator { get; set; }
}