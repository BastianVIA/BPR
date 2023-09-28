using BuildingBlocks.Domain;
using Domain.Events;

namespace Domain.Entities;

public class Actuator : Entity
{
    public CompositeActuatorId ActuatorId { get; private set; }
    public int PCBAUId { get; private set; }

    private Actuator()
    {
    }
    
    private Actuator(CompositeActuatorId actuatorId, int pcbaUid)
    {
        ActuatorId = actuatorId;
        PCBAUId = pcbaUid;
    }

    public static Actuator Create(CompositeActuatorId compositeActuatorId, int pcbaUid)
    {
        var actuator = new Actuator(compositeActuatorId, pcbaUid);
        actuator.AddDomainEvent(new ActuatorCreatedDomainEvent(actuator.ActuatorId));
        return actuator;
    }

}