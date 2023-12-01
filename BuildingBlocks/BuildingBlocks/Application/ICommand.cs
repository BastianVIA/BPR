using MediatR;

namespace BuildingBlocks.Application;

// All commands should have "private set" properties, in case they are used in inbox pattern where private set is needed
public interface ICommand:IRequest
{
}