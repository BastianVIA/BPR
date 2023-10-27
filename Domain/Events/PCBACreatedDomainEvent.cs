using BuildingBlocks.Domain;

namespace Domain.Events;

public class PCBACreatedDomainEvent : DomainEvent
{
    public int Id { get; }
    
    public PCBACreatedDomainEvent(int id)
    {
        Id = id;
    }
}