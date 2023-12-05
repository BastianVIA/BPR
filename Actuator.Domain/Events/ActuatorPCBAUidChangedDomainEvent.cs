using BuildingBlocks.Domain;
using Domain.Entities;

namespace Domain.Events;

public class ActuatorPCBAUidChangedDomainEvent : DomainEvent
{
    public CompositeActuatorId Id { get; }
    public string OldPCBAUid { get; }

    public ActuatorPCBAUidChangedDomainEvent(CompositeActuatorId id, string oldPcbaUid)
    {
        Id = id;
        OldPCBAUid = oldPcbaUid;
    }
}