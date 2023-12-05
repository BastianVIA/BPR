using BuildingBlocks.Domain;
using Domain.Entities;

namespace Domain.Events;

public class ActuatorCreatedDomainEvent : DomainEvent
{
    public CompositeActuatorId Id { get; }
    public string PCBAUid { get; }
    public string CommunicationProtocol { get; }
    public string ArticleNumber { get; }
    public string ArticleName { get; }
    public DateTime CreatedTime { get; }

    public ActuatorCreatedDomainEvent(CompositeActuatorId id, string pcbaUid, string articleNo,
        string articleName, string communicationProtocol, DateTime createdTime)
    {
        Id = id;
        PCBAUid = pcbaUid;
        ArticleNumber = articleNo;
        ArticleName = articleName;
        CommunicationProtocol = communicationProtocol;
        CreatedTime = createdTime;
    }
}