using MediatR;

namespace BuildingBlocks.Application;

public class QueryBus : IQueryBus
{
    private readonly ISender sender;

    public QueryBus(ISender sender)
    {
        this.sender = sender;
    }

    public Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellation)
    {
        return sender.Send<TResponse>(query, cancellation);
    }
}