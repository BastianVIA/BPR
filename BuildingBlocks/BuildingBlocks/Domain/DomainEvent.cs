namespace BuildingBlocks.Domain;

public class DomainEvent : IDomainEvent
{
    public Guid Id { get; } = default!;
    public DateTime OccurredOn { get; } = default!;
    
    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.Now;
    }
    
}