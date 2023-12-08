using BuildingBlocks.Domain;

namespace BuildingBlocks.Integration.Inbox.Events;

public class InboxMessageExitedFailLimitDomainEvent : DomainEvent
{
    public Guid Id { get; set; }

    public InboxMessageExitedFailLimitDomainEvent(Guid id)
    {
        Id = id;
    }
}