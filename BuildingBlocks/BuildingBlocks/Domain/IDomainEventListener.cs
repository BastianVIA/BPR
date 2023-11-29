using MediatR;

namespace BuildingBlocks.Domain;

public interface IDomainEventListener<in TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
}
