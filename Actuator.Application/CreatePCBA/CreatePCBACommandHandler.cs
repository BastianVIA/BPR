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
        var pcba = new PCBA(request.Uid, request.ManufacturerNumber, request.ItemNumber, request.Software, request.ProductionDateCode, request.ConfigNo);
        try
        {
            await _pcbaRepository.UpdatePCBA(pcba);
        }
        catch (KeyNotFoundException e)
        {
            var newPCBA = PCBA.Create(request.Uid, request.ManufacturerNumber, request.ItemNumber, request.Software,
                request.ProductionDateCode, request.ConfigNo);
            await _pcbaRepository.CreatePCBA(newPCBA);
        }
    }
}