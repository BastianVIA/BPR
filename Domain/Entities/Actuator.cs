using BuildingBlocks.Domain;
using Domain.Events;

namespace Domain.Entities;

public class Actuator : Entity
{
    public CompositeActuatorId Id { get; private set; }
    public int PCBAUid { get; private set; }

    private Actuator()
    {
    }
    
    public Actuator(CompositeActuatorId id, int pcbaUid)
    {
        Id = id;
        PCBAUid = pcbaUid;
    }

    public static Actuator Create(CompositeActuatorId id, int pcbaUid)
    {
        var actuator = new Actuator(id, pcbaUid);
        actuator.AddDomainEvent(new ActuatorCreatedDomainEvent(actuator.Id));
        return actuator;
    }

}