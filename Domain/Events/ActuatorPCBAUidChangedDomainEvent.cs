using BuildingBlocks.Domain;
using Domain.Entities;

namespace Domain.Events;

public class ActuatorPCBAUidChangedDomainEvent : DomainEvent
{
    public CompositeActuatorId Id { get; }

    public ActuatorPCBAUidChangedDomainEvent(CompositeActuatorId id)
    {
        Id = id;
    }
}