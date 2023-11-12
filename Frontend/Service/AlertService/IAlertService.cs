using Radzen;

namespace Frontend.Service.AlertService;

public interface IAlertService
{
    void FireEvent(AlertStyle style, string message);
}