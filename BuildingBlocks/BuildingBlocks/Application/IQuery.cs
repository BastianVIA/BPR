using MediatR;

namespace BuildingBlocks.Application;

public interface IQuery<out T> : IRequest<T>
{
}