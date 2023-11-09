using BuildingBlocks.Domain;
using MediatR;

namespace BuildingBlocks.Infrastructure;

public class Scheduler : IScheduler
{
    private IMediator _mediator;
    private Queue<IDomainEvent> eventQueue = new();

    public Scheduler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublishEvents()
    {
        while (eventQueue.Count > 0)
        {
            var eventsToPublish = eventQueue.ToArray();
            eventQueue.Clear();
            foreach (var domainEvent in eventsToPublish)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }

    public void QueueEvents(IList<IDomainEvent> events)
    {
        foreach (var domainEvent in events)
        {
            eventQueue.Enqueue(domainEvent);
        }
    }
}