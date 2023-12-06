using BuildingBlocks.Domain;

namespace TestResult.Domain.Events;

public class TestErrorCreatedDomainEvent : DomainEvent
{
    public Guid Id { get; }

    public TestErrorCreatedDomainEvent(Guid id)
    {
        Id = id;
    }
}