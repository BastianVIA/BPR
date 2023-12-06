using Frontend.Components;
using Frontend.Entities;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Frontend.Pages;

public class InformationContainerBase : ComponentBase
{
    [Parameter] public Actuator Actuator { get; set; }
    [Inject] public DialogService DialogService { get; set; }

    protected async Task OnChangeComponentBtnClick()
    {
        await DialogService.OpenAsync<ChangeComponent>($"Change Component",
            new Dictionary<string, object>() { { "Actuator", Actuator } },
            new DialogOptions() { Width = "600px", Height = "400px", Resizable = true, Draggable = true });
    }
}