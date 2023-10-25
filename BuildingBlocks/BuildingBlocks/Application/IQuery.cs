using MediatR;

namespace BuildingBlocks.Application;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}