using BuildingBlocks.Domain;
using Domain.Entities;
using Domain.Events;
using Domain.Repositories;

namespace Application.NewPCBAInActuator;

public class ActuatorPCBAUidChanged : IDomainEventListener<ActuatorPCBAUidChangedDomainEvent>
{
    private readonly IActuatorPCBAHistory _actuatorPcbaHistory;

    public ActuatorPCBAUidChanged(IActuatorPCBAHistory actuatorPcbaHistory)
    {
        _actuatorPcbaHistory = actuatorPcbaHistory;
    }

    public async Task Handle(ActuatorPCBAUidChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        var newChange = ActuatorPCBAChange.Create(notification.Id.WorkOrderNumber, notification.Id.SerialNumber,
            notification.OldPCBAUid, DateTime.Now);
        await _actuatorPcbaHistory.PCBARemoved(newChange);
    }
}