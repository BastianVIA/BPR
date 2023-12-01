﻿using BuildingBlocks.Domain;
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
        actuator.AddDomainEvent(new ActuatorCreatedDomainEvent(actuator.Id, pcba.Uid));
        return actuator;
    }

    public void UpdatePCBA(PCBA pcba)
    {
        PCBA = pcba;
        AddDomainEvent(new ActuatorPCBAUidChangedDomainEvent(Id));
    }
}