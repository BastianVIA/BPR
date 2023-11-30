using BuildingBlocks.Application;
using BuildingBlocks.Integration;
using LINTest.Integration;

namespace Application.CreateOrUpdateActuator;

public class ActuatorFound : IIntegrationEventListener<ActuatorFoundIntegrationEvent>
{
    private ICommandBus _bus;

    public ActuatorFound(ICommandBus bus)
    {
        _bus = bus;
    }

    public Task Handle(ActuatorFoundIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var cmd = CreateOrUpdateActuatorCommand.Create(notification.WorkOrderNumber, notification.SerailNumber,
            notification.PCBAUid);
        return _bus.Send(cmd, cancellationToken);
    }
}