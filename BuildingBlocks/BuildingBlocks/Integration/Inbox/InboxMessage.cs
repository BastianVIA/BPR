using BuildingBlocks.Application;
using BuildingBlocks.Integration.Inbox.Serialization;

namespace BuildingBlocks.Integration.Inbox;

public class InboxMessage
{
    public Guid Id { get; }
    public Guid IntegrationEventId { get; }
    public DateTime OccurredOn { get; }
    public string MessageType { get; } = default!;
    public string Payload { get; } = default!;
    public DateTime? ProcessedDate { get; private set; }
    public int FailedAttempts { get; private set; }
    public string? FailureReason { get; private set; }

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
        DateTime? processedDate, int failedAttempts, string? failureReason)
    {
        Id = id;
        IntegrationEventId = integrationEventId;
        OccurredOn = occurredOn;
        MessageType = messageType;
        Payload = payload;
        ProcessedDate = processedDate;
        FailedAttempts = failedAttempts;
        FailureReason = failureReason;
    }

    internal void Processed()
    {
        ProcessedDate = DateTime.Now;
    }

    internal void Failed(string failureReason)
    {
        FailureReason = failureReason;
        FailedAttempts++;
    }

    public static InboxMessage From(ICommand cmd, Guid integrationEventId)
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