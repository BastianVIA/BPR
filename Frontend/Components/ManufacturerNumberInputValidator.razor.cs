using Frontend.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class ManufacturerNumberInputValidatorBase : ComponentBase
{
    [Parameter] public Actuator Actuator { get; set; }
}