using Frontend.Events;

namespace Frontend.Service;

public interface IAlertService
{
    void FireEvent(AlertType type);
}