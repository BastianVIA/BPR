using Frontend.Entities;
using Frontend.Model;
using Frontend.Service.AlertService;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Frontend.Components;

public class ChangeComponentBase : ComponentBase
{
    [Parameter] public Actuator Actuator { get; set; }

    [Inject] public IAlertService AlertService { get; set; }
        
    [Inject] public IUpdateActuatorsPCBAModel UpdateActuatorsPcbaModel { get; set; }
    protected int PCBAUid { get; set; }

    protected string component;
    protected IEnumerable<string> componentNames = new []{"PCBA"};

    protected override Task OnInitializedAsync()
    {
        PCBAUid = Int32.Parse(Actuator.PCBA.PCBAUid);
        return base.OnInitializedAsync();
    }

    protected async Task OnSubmitComponentBtnClick()
    {
        switch (component)
        { 
            case null:
                AlertService.FireEvent(AlertStyle.Danger, "Missing component parameters");
                break;
            case "PCBA":
                if (PCBAUid == 0) break;
                try
                {
                    await UpdateActuatorsPcbaModel.UpdateActuatorsPCBA(Actuator.WorkOrderNumber,
                        Actuator.SerialNumber, PCBAUid.ToString());
                    AlertService.FireEvent(AlertStyle.Success, "PCBA change success"); 
                }
                catch (Exception e)
                {
                    AlertService.FireEvent(AlertStyle.Danger, "PCBA change failed");
                }
                break;
        }
        
    }
}