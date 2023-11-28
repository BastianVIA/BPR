using BuildingBlocks.Application;
using BuildingBlocks.Exceptions;
using Domain.Entities;
using Domain.Repositories;

namespace Application.CreatePCBA;

public class CreatePCBACommandHandler : ICommandHandler<CreatePCBACommand>
{
    private IPCBARepository _pcbaRepository;

    public CreatePCBACommandHandler(IPCBARepository pcbaRepository)
    {
        _pcbaRepository = pcbaRepository;
    }
    
    public async Task Handle(CreatePCBACommand request, CancellationToken cancellationToken)
    {
        try
        {
            var pcba = new PCBA(request.Uid, request.ManufacturerNumber);
            await _pcbaRepository.CreatePCBA(pcba);
        }
        catch (AlreadyExistingException e)
        {
            var pcba = await _pcbaRepository.GetPCBA(request.Uid);
            await _pcbaRepository.UpdatePCBA(pcba);
        }
    }
}