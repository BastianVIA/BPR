using BuildingBlocks.Application;
using BuildingBlocks.Integration;
using LINTest.Integration;

namespace Application.CreateOrUpdateActuator;

public class ActuatorTestSucceeded : IIntegrationEventListener<ActuatorTestSucceededIntegrationEvent>
{
    private ICommandBus _bus;

    public ActuatorTestSucceeded(ICommandBus bus)
    {
        _bus = bus;
    }

    public Task Handle(ActuatorTestSucceededIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var cmd = CreateOrUpdateActuatorCommand.Create(notification.WorkOrderNumber, notification.SerailNumber,
            notification.PCBAUid);
        return _bus.Send(cmd, cancellationToken);    }
}