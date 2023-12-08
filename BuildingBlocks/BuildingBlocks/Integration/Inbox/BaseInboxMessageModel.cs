namespace BuildingBlocks.Integration.Inbox;

public class BaseInboxMessageModel
{
    public Guid Id { get; set; }
    public Guid IntegrationEventId { get; set; }
    public DateTime OccurredOn { get; set;  }
    public string MessageType { get; set; }
    public string Payload { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public int FailedAttempts { get; set; }
    public string? FailureReason { get; set; }
    public bool IsFailing { get; set; }
}