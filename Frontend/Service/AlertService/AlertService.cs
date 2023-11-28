using Radzen;

namespace Frontend.Service.AlertService;

public class AlertService : IAlertService
{
    public static event Action<Alert>? OnAlertEvent;
    
    private void Invoke(Alert alert)
    {
        OnAlertEvent?.Invoke(alert);
    }

    public void FireEvent(AlertStyle style, string message)
    {
        var alert = new Alert
        {
            Message = message,
            Style = AlertStyle.Danger
        };
        Invoke(alert);
    }
}