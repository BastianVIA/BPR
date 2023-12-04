using BuildingBlocks.Application;
using BuildingBlocks.Infrastructure.Database.Transaction;
using Domain.Entities;
using Domain.Repositories;

namespace Application.CreateActuator;

public class CreateActuatorCommandHandler : ICommandHandler<CreateActuatorCommand>
{
    private IActuatorRepository _actuatorRepository;
    private IPCBARepository _pcbaRepository;
    private IDbTransaction _dbTransaction;

    public CreateActuatorCommandHandler(IActuatorRepository actuatorRepository, IPCBARepository pcbaRepository,
        IDbTransaction dbTransaction)
    {
        _actuatorRepository = actuatorRepository;
        _pcbaRepository = pcbaRepository;
        _dbTransaction = dbTransaction;
    }

    public async Task Handle(CreateActuatorCommand request, CancellationToken cancellationToken)
    {
        var pcba = await GetPCBA(request.PCBAUid);
        var actuatorId = CompositeActuatorId.From(request.WorkOrderNumber, request.SerialNumber);
        var actuator = Actuator.Create(actuatorId, pcba, request.ArticleNumber, request.ArticleName, request.CommunicationProtocol,request.CreatedTime);
        await _actuatorRepository.CreateActuator(actuator);
        await _dbTransaction.CommitAsync(cancellationToken);
    }

    private async Task<PCBA> GetPCBA(string pcbaUid)
    {
        var pcba = new PCBA(pcbaUid);
        try
        {
            pcba = await _pcbaRepository.GetPCBA(pcbaUid);
        }
        catch (KeyNotFoundException e)
        {
            await _pcbaRepository.CreatePCBA(pcba);
            pcba = await _pcbaRepository.GetPCBA(pcbaUid);
        }

        return pcba;
    }
}