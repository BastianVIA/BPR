using System.ComponentModel.DataAnnotations;
using BuildingBlocks.Application;
using BuildingBlocks.Domain;
using BuildingBlocks.Integration.Inbox.Events;
using BuildingBlocks.Integration.Inbox.Serialization;

namespace BuildingBlocks.Integration.Inbox;

public class InboxMessage : Entity
{
    public Guid Id { get; }
    public Guid IntegrationEventId { get; }
    public DateTime OccurredOn { get; }
    public string MessageType { get; } = default!;
    public string Payload { get; } = default!;
    public DateTime? ProcessedDate { get; private set; }
    public int FailedAttempts { get; private set; }
    public string? FailureReason { get; private set; }
    public bool IsFailing { get; private set; }

    private InboxMessage() { }

    private InboxMessage(Guid id, DateTime occurredOn, string messageType, string payload, Guid integrationEventId)
    {
        Id = id;
        OccurredOn = occurredOn;
        MessageType = messageType;
        Payload = payload;
        IntegrationEventId = integrationEventId;
    }

    //This constructor is only to be used when getting from the database
    public InboxMessage(Guid id, DateTime occurredOn, Guid integrationEventId, string messageType, string payload,
        DateTime? processedDate, int failedAttempts, string? failureReason, bool isFailing)
    {
        Id = id;
        IntegrationEventId = integrationEventId;
        OccurredOn = occurredOn;
        MessageType = messageType;
        Payload = payload;
        ProcessedDate = processedDate;
        FailedAttempts = failedAttempts;
        FailureReason = failureReason;
        IsFailing = isFailing;
    }

    internal void Processed()
    {
        ProcessedDate = DateTime.Now;
    }

    internal void Failed(string failureReason)
    {
        FailureReason = failureReason;
        FailedAttempts++;
        if (FailedAttempts > 1 && !IsFailing)
        {
            ProcessedDate = DateTime.Now;
            AddDomainEvent(new InboxMessageExitedFailLimitDomainEvent(Id));
        }
    }

    internal void MarkFailedBackToAsNotProcessed()
    {
        if (ProcessedDate == null)
        {
            throw new ValidationException(
                "Cannot MarkFailedBackToAsNotProcessed, on something that has not yet been processed");
        }

        ProcessedDate = null;
        IsFailing = true;
    }

    public static InboxMessage Create(ICommand cmd, Guid integrationEventId)
    {
        if (cmd is null)
        {
            throw new ArgumentNullException(nameof(cmd));
        }

        var messageType = cmd.GetType().AssemblyQualifiedName;
        var type = Type.GetType(messageType!);

        var serializer = new InboxMessageSerializer();
        var payload = serializer.Serialize(cmd, type);
        var id = Guid.NewGuid();
        return new InboxMessage(id, DateTime.Now, messageType!, payload, integrationEventId);
    }
}