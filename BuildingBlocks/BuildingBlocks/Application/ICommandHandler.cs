using MediatR;

namespace BuildingBlocks.Application;

public interface ICommandHandler<TRequest>: IRequestHandler<TRequest> where TRequest: IRequest
{
}