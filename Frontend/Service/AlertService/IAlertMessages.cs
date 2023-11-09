namespace Frontend.Service.AlertService;

public interface IAlertMessages
{
    public Alert NetworkError();
    public Alert ActuatorDetailsFailure();
}