using MediatR;

namespace BuildingBlocks.Integration;

public interface IIntegrationEventListener<in TIntegrationEvent> : INotificationHandler<TIntegrationEvent> where TIntegrationEvent : IIntegrationEvent
{ }