using BuildingBlocks.Domain;
using Domain.Entities;

namespace Domain.Events;

public class ActuatorCreatedDomainEvent : DomainEvent
{
    public CompositeActuatorId Id { get; }

    public ActuatorCreatedDomainEvent(CompositeActuatorId id)
    {
        Id = id;
    }
}