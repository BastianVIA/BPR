using Frontend.Events;

namespace Frontend.UnitTest.Events;

public class AlertTypeTests
{

    [Fact]
    public void AlertType_ActuatorDetailsFailure_HasDefinedAlertMessage()
    {
        var type = AlertType.ActuatorDetailsFailure;
        var method = typeof(AlertMessages).GetMethod(type.ToString());
        Assert.NotNull(method);
    }
    
    [Fact]
    public void AlertType_NetworkError_HasDefinedAlertMessage()
    {
        var type = AlertType.NetworkError;
        var method = typeof(AlertMessages).GetMethod(type.ToString());
        Assert.NotNull(method);
    }
}