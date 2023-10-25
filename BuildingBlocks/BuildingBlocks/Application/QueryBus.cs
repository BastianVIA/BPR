using MediatR;

namespace BuildingBlocks.Application;

public class QueryBus : IQueryBus
{
    private readonly ISender _sender;

    public QueryBus(ISender sender)
    {
        _sender = sender;
    }

    public Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellation)
    {
        return _sender.Send<TResponse>(query, cancellation);
    }
}