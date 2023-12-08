using Frontend.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components.Validators;

public class PCBAUidInputValidatorBase : ComponentBase
{
    [Parameter] public Actuator Actuator { get; set; }
}