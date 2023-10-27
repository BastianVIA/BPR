using BuildingBlocks.Application;
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
            var pcba = PCBA.Create(request.PCBAUid, request.ManufacturerNumber);
            await _pcbaRepository.CreatePCBA(pcba);
        } catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}