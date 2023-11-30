namespace BuildingBlocks.Integration;

public class IntegrationEvent : IIntegrationEvent
{
    public Guid Id { get; } = default!;
    public DateTime OccurredOn { get; } = default!;
    
    
    protected IntegrationEvent()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.Now;
    }
}