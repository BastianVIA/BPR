using BuildingBlocks.Domain;

namespace TestResult.Domain.Events;

public class TestResultCreatedDomainEvent : DomainEvent
{
    public Guid Id { get; }

    public TestResultCreatedDomainEvent(Guid id)
    {
        Id = id;
    }
}