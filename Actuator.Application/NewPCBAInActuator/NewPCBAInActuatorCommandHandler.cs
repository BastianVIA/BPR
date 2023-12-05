using BuildingBlocks.Application;
using BuildingBlocks.Infrastructure.Database.Transaction;
using Domain.Entities;
using Domain.Repositories;
using LINTest.LinakDB;

namespace Application.NewPCBAInActuator;

public class NewPCBAInActuatorCommandHandler : ICommandHandler<NewPCBAInActuatorCommand>
{
    private readonly IActuatorRepository _actuatorRepository;
    private readonly IPCBARepository _pcbaRepository;
    private readonly IDbTransaction _dbTransaction;
    private readonly IPCBAService _pcbaService;

    public NewPCBAInActuatorCommandHandler(IActuatorRepository actuatorRepository, IPCBARepository pcbaRepository,
        IDbTransaction dbTransaction, IPCBAService pcbaService)
    {
        _actuatorRepository = actuatorRepository;
        _pcbaRepository = pcbaRepository;
        _dbTransaction = dbTransaction;
        _pcbaService = pcbaService;
    }

    public async Task Handle(NewPCBAInActuatorCommand request, CancellationToken cancellationToken)
    {
        var actuatorId = CompositeActuatorId.From(request.WorkOrderNumber, request.SerialNumber);

        var actuatorToUpdate = await _actuatorRepository.GetActuator(actuatorId);
        try
        {
            var newPCBA = await _pcbaRepository.GetPCBA(request.PCBAUid);
            actuatorToUpdate.UpdatePCBA(newPCBA);
        }
        catch (KeyNotFoundException e)
        {
            var pcbaFromService = _pcbaService.GetPCBA(request.PCBAUid);
            var domainPcba = PCBA.Create(pcbaFromService.Uid.ToString(), pcbaFromService.ManufacturerNumber,
                pcbaFromService.ItemNumber,
                pcbaFromService.Software, pcbaFromService.ProductionDateCode, pcbaFromService.ConfigNo);
            await _pcbaRepository.CreatePCBA(domainPcba);
            actuatorToUpdate.UpdatePCBA(domainPcba);
        }

        await _actuatorRepository.UpdateActuator(actuatorToUpdate);

        await _dbTransaction.CommitAsync(cancellationToken);
    }
}