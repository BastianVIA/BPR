using BuildingBlocks.Domain;
using Domain.Events;

namespace Domain.Entities;

public class Actuator : Entity
{
    public CompositeActuatorId Id { get; private set; }
    public PCBA PCBA { get; private set; }

    private Actuator()
    {
    }
    
    public Actuator(CompositeActuatorId id, PCBA pcba)
    {
        Id = id;
        PCBA = pcba;
    }

    public static Actuator Create(CompositeActuatorId id, PCBA pcba)
    {
        var actuator = new Actuator(id, pcba);
        actuator.AddDomainEvent(new ActuatorCreatedDomainEvent(actuator.Id));
        return actuator;
    }

    public void UpdatePCBAUid(int pcbaUid)
    {
        PCBAUid = pcbaUid;
        AddDomainEvent(new ActuatorPCBAUidChangedDomainEvent(Id));
    }
}