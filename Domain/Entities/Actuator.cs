using BuildingBlocks.Domain;
using Domain.Events;

namespace Domain.Entities;

public class Actuator : Entity
{
    public CompositeActuatorId ActuatorId { get; private set; }
    public string PCBAId { get; private set; }

    private Actuator()
    {
    }
    
    private Actuator(CompositeActuatorId actuatorId, string pcbaId)
    {
        ActuatorId = actuatorId;
        PCBAId = pcbaId;
    }

    public static Actuator Create(CompositeActuatorId compositeActuatorId, string pcbaId)
    {
        var actuator = new Actuator(compositeActuatorId, pcbaId);
        actuator.AddDomainEvent(new ActuatorCreatedDomainEvent(actuator.ActuatorId));
        return actuator;
    }

}