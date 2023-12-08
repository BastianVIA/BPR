using Frontend.Entities;
using Frontend.Model;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class ComponentHistoryBase : ComponentBase
{
    [Parameter] public Actuator Actuator { get; set; }
    [Inject] public IActuatorComponentHistoryModel ActuatorComponentHistoryModel { get; set; }
    protected List<ComponentChange> ComponentChanges { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ComponentChanges =
            await ActuatorComponentHistoryModel.GetComponentHistory(Actuator.WorkOrderNumber, Actuator.SerialNumber);
        ComponentChanges.Sort((cc1, cc2) => DateTime.Compare(cc1.RemovalTime, cc2.RemovalTime));
    }
}