using Frontend.Entities;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public class PCBAInformationBase : ComponentBase
{
    [Parameter] public Actuator Actuator { get; set; }
}