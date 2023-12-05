using Frontend.Service.AlertService;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components;

public class AlertBoxBase : ComponentBase, IDisposable
{
    protected Alert? Alert { get; set; } = null;
    private int _alertTimeSeconds = 5;
    private CancellationTokenSource? _cancelAlert;

    protected override void OnInitialized()
    {
        AlertService.OnAlertEvent += HandleAlert;
    }
    
    public void Dispose()
    {
        AlertService.OnAlertEvent -= HandleAlert;
    }

    private void HandleAlert(Alert alert)
    {
        Alert = alert;
        InvokeAsync(StateHasChanged);
        HideAlertTimer();
    }
    
    private void HideAlertTimer()
    {
        _cancelAlert?.Cancel();
        _cancelAlert = new CancellationTokenSource();
        var cancelToken = _cancelAlert.Token;
        Task.Delay(_alertTimeSeconds * 1000, cancelToken).ContinueWith(_ =>
        {
            if (!cancelToken.IsCancellationRequested)
            {
                Alert = null;
                InvokeAsync(StateHasChanged);
            }
        });
    }
}