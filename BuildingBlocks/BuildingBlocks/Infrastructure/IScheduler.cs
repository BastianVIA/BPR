using BuildingBlocks.Domain;

namespace BuildingBlocks.Infrastructure;

public interface IScheduler
{
    public Task PublishEvents();
    public void QueueEvents(IList<IDomainEvent> events);
}