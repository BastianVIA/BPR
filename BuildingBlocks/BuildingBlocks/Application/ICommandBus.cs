namespace BuildingBlocks.Application;

public interface ICommandBus
{
    Task Send(ICommand command, CancellationToken cancellation);
}