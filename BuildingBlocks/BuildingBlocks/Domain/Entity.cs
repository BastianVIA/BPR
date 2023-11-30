namespace BuildingBlocks.Domain;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IList<IDomainEvent> GetDomainEvents()
    {
        var copyOfDomainEvents = new List<IDomainEvent>(_domainEvents);
        _domainEvents.Clear();
        return copyOfDomainEvents;
    }
}