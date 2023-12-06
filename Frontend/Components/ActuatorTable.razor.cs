using Frontend.Entities;
using Frontend.Service;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class ActuatorTableBase : ComponentBase
{
    [Parameter] public List<Actuator> Actuators { get; set; } = new();
    [Parameter] public EventCallback<Actuator> OnActuatorSelected { get; set; }
    [Parameter] public EventCallback<List<CsvProperties>> OnColumnsUpdated { get; set; }
    [Parameter] public EventCallback<List<string>> OnDownloadSelected { get; set; }

    protected List<CsvProperties> FilterOptions { get; } = new (){
        CsvProperties.Work_Order_Number,
        CsvProperties.Serial_Number,
        CsvProperties.Communication_Protocol,
        CsvProperties.Article_Number,
        CsvProperties.Article_Name,
        CsvProperties.Created_Time,
        CsvProperties.PCBA_Uid,
        CsvProperties.PCBA_Manufacturer_Number,
        CsvProperties.PCBA_Item_Number,
        CsvProperties.PCBA_Software,
        CsvProperties.PCBA_Production_DateCode,
        CsvProperties.PCBA_Config_No,
    };
    protected List<CsvProperties> Filters { get; set; } = new()
    {
        CsvProperties.Work_Order_Number,
        CsvProperties.Serial_Number,
        CsvProperties.PCBA_Uid,
    };

    protected void UpdateFilters(List<CsvProperties>? filters)
    {
        filters ??= new List<CsvProperties>();
        Filters = filters;
        OnColumnsUpdated.InvokeAsync(filters);
    }

    protected bool ShouldShowColumn(CsvProperties name)
    {
        return Filters.Contains(name);
    }

    protected void SelectActuator(Actuator actuator)
    {
        OnActuatorSelected.InvokeAsync(actuator);
    }

    public void DownloadCsv()
    {
        OnDownloadSelected.InvokeAsync(Filters);
    }
}