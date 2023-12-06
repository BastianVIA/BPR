using BuildingBlocks.Domain;
using Domain.Events;

namespace Domain.Entities;

public class Actuator : Entity
{
    public CompositeActuatorId Id { get; private set; }
    public PCBA PCBA { get; private set; }
    public string CommunicationProtocol { get; private set; }
    public string ArticleNumber { get; private set; }
    public string ArticleName { get; private set; }
    public DateTime CreatedTime { get; private set; }

    private Actuator()
    {
    }
    
    public Actuator(CompositeActuatorId id, PCBA pcba, string articleNo,
        string articleName, string communicationProtocol, DateTime createdTime)
    {
        Id = id;
        PCBA = pcba;
        ArticleNumber = articleNo;
        ArticleName = articleName;
        CommunicationProtocol = communicationProtocol;
        CreatedTime = createdTime;
    }

    public static Actuator Create(CompositeActuatorId id, PCBA pcba, string articleNo,
        string articleName, string communicationProtocol, DateTime createdTime)
    {
        var actuator = new Actuator(id, pcba, articleNo, articleName, communicationProtocol, createdTime);
        actuator.AddDomainEvent(new ActuatorCreatedDomainEvent(actuator.Id, pcba.Uid, actuator.ArticleNumber, actuator.ArticleName, actuator.CommunicationProtocol, actuator.CreatedTime));
        return actuator;
    }

    public void UpdatePCBA(PCBA pcba)
    {
        AddDomainEvent(new ActuatorPCBAUidChangedDomainEvent(Id, PCBA.Uid));
        PCBA = pcba;
    }
}