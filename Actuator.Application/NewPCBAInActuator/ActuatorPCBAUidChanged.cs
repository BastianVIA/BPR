using BuildingBlocks.Domain;
using Domain.Entities;
using Domain.Events;
using Domain.Repositories;

namespace Application.NewPCBAInActuator;

public class ActuatorPCBAUidChanged : IDomainEventListener<ActuatorPCBAUidChangedDomainEvent>
{
    private readonly IActuatorPCBAHistoryRepository actuatorPcbaHistoryRepository;

    public ActuatorPCBAUidChanged(IActuatorPCBAHistoryRepository actuatorPcbaHistoryRepository)
    {
        this.actuatorPcbaHistoryRepository = actuatorPcbaHistoryRepository;
    }

    public async Task Handle(ActuatorPCBAUidChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        var newChange = ActuatorPCBAChange.Create(notification.Id.WorkOrderNumber, notification.Id.SerialNumber,
            notification.OldPCBAUid, DateTime.Now);
        await actuatorPcbaHistoryRepository.PCBARemoved(newChange);
    }
}