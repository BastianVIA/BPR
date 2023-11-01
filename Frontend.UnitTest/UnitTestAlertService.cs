using Frontend.Events;
using Frontend.Service;
using NSubstitute;
using Radzen;

namespace Frontend.UnitTest;

public class UnitTestAlertService
{
    [Fact]
    public void FireEvent_InvokesCorrectAlertMethod()
    {
        // Arrange
        var mockMessages = Substitute.For<IAlertMessages>();
        mockMessages.NetworkError().Returns(Alert.Create("Network error", AlertStyle.Danger));

        var alertService = new AlertService(mockMessages);

        // Act
        alertService.FireEvent(AlertType.NetworkError);

        // Assert
        mockMessages.Received().NetworkError();
    }

    [Fact]
    public void FireEvent_RaisesOnAlertEvent()
    {
        // Arrange
        var mockMessages = Substitute.For<IAlertMessages>();
        var alert = Alert.Create("Network error", AlertStyle.Danger);
        mockMessages.NetworkError().Returns(alert);

        var alertService = new AlertService(mockMessages);

        Alert? receivedAlert = null;
        AlertService.OnAlertEvent += a => receivedAlert = a;

        // Act
        alertService.FireEvent(AlertType.NetworkError);

        // Assert
        Assert.Equal(alert.Message, receivedAlert?.Message);
        Assert.Equal(alert.Style, receivedAlert?.Style);
    }


    [Fact]
    public void ActuatorDetailsFailure_ReturnsCorrectAlert()
    {
        // Arrange
        var alertMessages = new AlertMessages();

        // Act
        var alert = alertMessages.ActuatorDetailsFailure();

        // Assert
        Assert.Equal("Could not find actuator", alert.Message);
        Assert.Equal(AlertStyle.Danger, alert.Style);
    }

    [Fact]
    public void NetworkError_ReturnsCorrectAlert()
    {
        // Arrange
        var alertMessages = new AlertMessages();

        // Act
        var alert = alertMessages.NetworkError();

        // Assert
        Assert.Equal("Unknown network error", alert.Message);
        Assert.Equal(AlertStyle.Danger, alert.Style);
    }
}