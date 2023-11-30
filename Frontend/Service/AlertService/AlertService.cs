using Radzen;

namespace Frontend.Service.AlertService;

public class AlertService : IAlertService
{
    public delegate void AlertAction(Alert alert);
    public static event AlertAction? OnAlertEvent;
    
    private void Invoke(Alert alert)
    {
        OnAlertEvent?.Invoke(alert);
    }

    public void FireEvent(AlertStyle style, string message)
    {
        var alert = new Alert
        {
            Message = message,
            Style = style
        };
        Invoke(alert);
    }
}