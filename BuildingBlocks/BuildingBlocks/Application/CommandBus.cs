using MediatR;

namespace BuildingBlocks.Application;

public class CommandBus : ICommandBus
{
    private readonly ISender sender;


    public CommandBus(ISender sender)
    {
        this.sender = sender;
    }

    public async Task Send(ICommand command, CancellationToken cancellation)
    {
        await sender.Send(command, cancellation);
    }

}