namespace Frontend.Service.AlertService;

public interface IAlertService
{
    void FireEvent(AlertType type);
}