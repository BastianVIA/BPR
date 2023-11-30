using Frontend.Entities;
using Frontend.Model;
using Microsoft.AspNetCore.Components;

namespace Frontend.Pages;

public class ActuatorsInfoBase: ComponentBase
{
    private int? itemNumberFilter;
    private int? manufacturerNumberFilter;
    private int? productionDateCodeFilter;
    public List<Actuator> actuators = new();
    public Actuator SearchActuator = new Actuator();

    [Inject]
    public IGetActuatorsWithFilterModel GetActuatorsWithFilter { get; set; }


    public async Task SearchWithFilter()
    {
        actuators = await GetActuatorsWithFilter.GetActuatorWithFilter(SearchActuator.PCBA.ItemNumber, SearchActuator.PCBA.ManufacturerNumber, SearchActuator.PCBA.ProductionDateCode);
    }

} 