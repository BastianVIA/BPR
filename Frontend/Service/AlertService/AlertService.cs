namespace Frontend.Service.AlertService;

public class AlertService : IAlertService
{
    public delegate void AlertAction(Alert alert);
    public static event AlertAction OnAlertEvent;
    private IAlertMessages _messages;
    
    public AlertService(IAlertMessages messages)
    {
       _messages = messages;
    }
    
    public void FireEvent(AlertType type)
    {
        var method = _messages.GetType().GetMethod($"{type.ToString()}");
        var invoked = method.Invoke(_messages, null);
    
        if (invoked is Alert alert)
            OnAlertEvent?.Invoke(alert); 
    }
}