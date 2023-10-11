using Radzen;

namespace Frontend.Events;

public class AlertMessages : IAlertMessages
{
    public Alert ActuatorDetailsSuccess() => 
        Alert.Create("Successfully found actuator details", AlertStyle.Success);

    public Alert ActuatorDetailsFailure() =>
        Alert.Create($"Could not find actuator", AlertStyle.Danger);

    public Alert NetworkError() =>
        Alert.Create("Encountered network error", AlertStyle.Danger);

}