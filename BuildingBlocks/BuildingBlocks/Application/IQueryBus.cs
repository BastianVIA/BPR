namespace BuildingBlocks.Application;

public interface IQueryBus
{
    Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellation);
}