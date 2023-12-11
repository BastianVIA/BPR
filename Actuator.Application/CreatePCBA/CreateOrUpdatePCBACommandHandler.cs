using BuildingBlocks.Application;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.CreatePCBA;

public class CreateOrUpdatePCBACommandHandler : ICommandHandler<CreateOrUpdatePCBACommand>
{
    private IPCBARepository _pcbaRepository;

    public CreateOrUpdatePCBACommandHandler(IPCBARepository pcbaRepository)
    {
        _pcbaRepository = pcbaRepository;
    }
    
    public async Task Handle(CreateOrUpdatePCBACommand request, CancellationToken cancellationToken)
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