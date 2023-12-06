using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class TableFiltersBase : ComponentBase
{
    // Faked enums, husk at skifte til den fra endpoint
    public enum CsvProperties
    {
        WorkOrderNumber,
        SerialNumber,
        CommunicationProtocol,
        ArticleNumber,
        ArticleName,
        CreatedTime,
        PCBAUid,
        PCBAManufacturerNumber,
        PCBAItemNumber,
        PCBASoftware,
        PCBAProductionDateCode,
        PCBAConfigNo,
    }
    [Parameter] public EventCallback<List<CsvProperties>> OnNewFilter { get; set; }
    [Parameter] public List<CsvProperties> FilterParams { get; set; }
    [Parameter] public List<CsvProperties> CurrentFilter { get; set; }

    protected override Task OnInitializedAsync()
    {
        OnChange();
        return base.OnInitializedAsync();
    }

    protected void OnChange()
    {
        OnNewFilter.InvokeAsync(CurrentFilter);
    }
}