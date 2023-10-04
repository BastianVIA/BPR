using Frontend.Events;

namespace Frontend.Service;

public interface IAlertService
{
    //public event EventHandler<Alert>? OnAlertEvent;
    void FireEvent(AlertType type);
}