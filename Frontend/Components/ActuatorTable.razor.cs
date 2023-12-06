using Frontend.Entities;
using Frontend.Util;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class ActuatorTableBase : ComponentBase
{
    [Parameter] public List<Actuator> Actuators { get; set; } = new();
    [Parameter] public EventCallback<Actuator> OnActuatorSelected { get; set; }
    [Parameter] public EventCallback<List<TableFiltersBase.CsvProperties>> OnColumnsUpdated { get; set; }
    
    protected List<TableFiltersBase.CsvProperties> FilterOptions { get; } = new (){
        TableFiltersBase.CsvProperties.WorkOrderNumber,
        TableFiltersBase.CsvProperties.SerialNumber,
        TableFiltersBase.CsvProperties.CommunicationProtocol,
        TableFiltersBase.CsvProperties.ArticleNumber,
        TableFiltersBase.CsvProperties.ArticleName,
        TableFiltersBase.CsvProperties.CreatedTime,
        TableFiltersBase.CsvProperties.PCBAUid,
        TableFiltersBase.CsvProperties.PCBAManufacturerNumber,
        TableFiltersBase.CsvProperties.PCBAItemNumber,
        TableFiltersBase.CsvProperties.PCBASoftware,
        TableFiltersBase.CsvProperties.PCBAProductionDateCode,
        TableFiltersBase.CsvProperties.PCBAConfigNo,
    };
    protected List<TableFiltersBase.CsvProperties> Filters { get; set; } = new()
    {
        TableFiltersBase.CsvProperties.WorkOrderNumber,
        TableFiltersBase.CsvProperties.SerialNumber,
        TableFiltersBase.CsvProperties.PCBAUid
    };

    protected void UpdateFilters(List<TableFiltersBase.CsvProperties>? filters)
    {
        filters ??= new List<TableFiltersBase.CsvProperties>();
        Filters = filters;
        OnColumnsUpdated.InvokeAsync(filters);
    }

    protected bool ShouldShowColumn(TableFiltersBase.CsvProperties name)
    {
        return Filters.Contains(name);
    }

    protected void SelectActuator(Actuator actuator)
    {
        OnActuatorSelected.InvokeAsync(actuator);
    }
}