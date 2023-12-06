using Frontend.Entities;
using Frontend.Service.AlertService;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Frontend.Components;

public class ChangeComponentBase : ComponentBase
{
        [Parameter] public Actuator Actuator { get; set; }
        
        [Inject] public IAlertService AlertService { get; set; }
        protected int PCBAUid { get; set; }

        protected string value;
        protected IEnumerable<string> componentNames = new []{"PCBA"};
        
        protected async Task OnSubmitComponentBtnClick()
        {
                switch (value)
                {
                        case null:
                                AlertService.FireEvent(AlertStyle.Danger, "Missing component parameters");
                                break;
                        case "PCBA":
                                if (PCBAUid > 0)
                                {
                                        
                                }
                                break;
                }

                return;
        }
}