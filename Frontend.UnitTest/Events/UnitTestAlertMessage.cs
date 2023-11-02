using Frontend.Events;

namespace Frontend.UnitTest;

public class UnitTestAlertMessage
{
    // Arrange
    private readonly AlertMessages _alertMessages = new();
    
    [Fact]
    public void ActuatorDetailsFailure_ReturnsCorrectAlert()
    {
        // Act
        var alert = _alertMessages.ActuatorDetailsFailure();

        // Assert
        Assert.Equal("Could not find actuator", alert.Message);
        Assert.Equal(AlertStyle.Danger, alert.Style);
    }

    [Fact]
    public void NetworkError_ReturnsCorrectAlert()
    {
        // Act
        var alert = _alertMessages.NetworkError();

        // Assert
        Assert.Equal("Unknown network error", alert.Message);
        Assert.Equal(AlertStyle.Danger, alert.Style);
    }
}