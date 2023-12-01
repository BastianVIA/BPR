using BuildingBlocks.Domain;
using Domain.Entities;

namespace Domain.Events;

public class ActuatorCreatedDomainEvent : DomainEvent
{
    public CompositeActuatorId Id { get; }
    public string PCBAUid { get; }

    public ActuatorCreatedDomainEvent(CompositeActuatorId id, string pcbaUid)
    {
        Id = id;
        PCBAUid = pcbaUid;
    }
}