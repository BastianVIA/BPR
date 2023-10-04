namespace Frontend.Events;

public class AlertMessages : IAlertMessages
{
    public Alert ActuatorDetailsSuccess()
    {
        return new Alert()
        {
            Message = "Det virk",
            IsSuccessful = true
        };
    }

}