using Radzen;

namespace Frontend.Events;

public class AlertMessages : IAlertMessages
{
   
    public Alert ActuatorDetailsFailure() =>
        Alert.Create("Could not find actuator", AlertStyle.Danger);

    public Alert NetworkError() =>
        Alert.Create("Unknown network error", AlertStyle.Danger);

}