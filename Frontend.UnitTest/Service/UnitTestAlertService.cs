using Frontend.Events;
using Frontend.Service;

namespace Frontend.UnitTest;

public class UnitTestAlertService
{
    private readonly IAlertMessages _mockMessages = Substitute.For<IAlertMessages>();
    private AlertService _alertService;
    public UnitTestAlertService()
    {
        _alertService = new AlertService(_mockMessages);

    }
    
    [Fact]
    public void FireEvent_CallsNetworkErrorMethod()
    {
        // Arrange
        _mockMessages.NetworkError().Returns(Alert.Create("Network error", AlertStyle.Danger));

        // Act
        _alertService.FireEvent(AlertType.NetworkError);

        // Assert
        _mockMessages.Received().NetworkError();
    }
    
    
    [Fact]
    public void FireEvent_RaisesOnAlertEventWithCorrectAlert()
    {
        // Arrange
        var expectedAlert = Alert.Create("Network error", AlertStyle.Danger);
        _mockMessages.NetworkError().Returns(expectedAlert);

        Alert? receivedAlert = null;
        AlertService.OnAlertEvent += a => receivedAlert = a;

        // Act
        _alertService.FireEvent(AlertType.NetworkError);

        // Assert
        Assert.NotNull(receivedAlert);
        Assert.Equal(expectedAlert.Message, receivedAlert?.Message);
        Assert.Equal(expectedAlert.Style, receivedAlert?.Style);
    }

   
}