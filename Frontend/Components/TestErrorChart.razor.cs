using Microsoft.AspNetCore.Components;
using Radzen;

namespace Frontend.Components;

public class TestErrorChartBase : ComponentBase
{
    [Parameter] public float Percentage { get; set; }
    [Parameter] public string ErrorCodeName { get; set; }
    protected ErrorDataItem[]? _errorPercentageData;
    protected bool showDataLabels = false;

    protected void OnSeriesClick(SeriesClickEventArgs args)
    {
    }

    protected class ErrorDataItem
    {
        public string ErrorType { get; set; }
        public float Percentage { get; set; }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        ErrorCodeName ??= "Default Error";
        Percentage = Percentage == 0 ? 100 : Percentage;

        _errorPercentageData = new ErrorDataItem[]
        {
            new ErrorDataItem { ErrorType = ErrorCodeName, Percentage = Percentage },
            new ErrorDataItem { ErrorType = "All Other Errors", Percentage = 100 - Percentage }
        };
    }
}