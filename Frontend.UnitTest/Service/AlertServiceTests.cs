using Frontend.Service.AlertService;

namespace Frontend.UnitTest.Service;

public class AlertServiceTests
{
    private Fixture _fixture = new();
    private AlertService _alertService;
    public AlertServiceTests()
    {
        _alertService = new AlertService();
    }

    [Fact]
    public void FireEvent_ShouldNotInvokeEvent_WhenEventHasNoListeners()
    {
        // Arrange
        var alert = _fixture.Create<Alert>();
        AlertEventTestDouble eventDouble = new ();
        
        // Act
        _alertService.FireEvent(alert.Style, alert.Message);
        // Assert
        Assert.False(eventDouble.OnAlertEventInvoked);
    }

    [Fact]
    public void FireEvent_ShouldInvokeEvent_WhenEventHasListeners()
    {
        // Arrange
        var expectedAlert = _fixture.Create<Alert>();
        AlertEventTestDouble mockListener = new ();
        
        mockListener.Attach();
        
        // Act
        _alertService.FireEvent(expectedAlert.Style, expectedAlert.Message);

        // Assert
        Assert.True(mockListener.OnAlertEventInvoked);
        Assert.Equal(expectedAlert.Style, mockListener.InvokedAlert.Style);
        Assert.Equal(expectedAlert.Message, mockListener.InvokedAlert.Message);
    }
}

public class AlertEventTestDouble : IDisposable
{
    public bool OnAlertEventInvoked { get; private set; }
    public Alert InvokedAlert { get; private set; }

    public void Attach()
    {
        AlertService.OnAlertEvent += AlertEventHandler;
    }

    private void AlertEventHandler(Alert alert)
    {
        OnAlertEventInvoked = true;
        InvokedAlert = alert;
    }

    public void Dispose()
    {
        AlertService.OnAlertEvent -= AlertEventHandler;
    }
}