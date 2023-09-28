using BuildingBlocks.Domain;
using Domain.Entities;

namespace Domain.Events;

public class ActuatorCreatedDomainEvent : DomainEvent
{
    public CompositeActuatorId ActuatorId { get; }

    public ActuatorCreatedDomainEvent(CompositeActuatorId actuatorId)
    {
        ActuatorId = actuatorId;
    }
}