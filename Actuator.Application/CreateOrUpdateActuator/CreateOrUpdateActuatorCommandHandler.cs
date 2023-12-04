using BuildingBlocks.Application;
using BuildingBlocks.Infrastructure.Database.Transaction;
using Domain.Entities;
using Domain.Repositories;

namespace Application.CreateOrUpdateActuator;

public class CreateOrUpdateActuatorCommandHandler : ICommandHandler<CreateOrUpdateActuatorCommand>
{
    private IActuatorRepository _actuatorRepository;
    private IPCBARepository _pcbaRepository;
    private IDbTransaction _dbTransaction;

    public CreateOrUpdateActuatorCommandHandler(IActuatorRepository actuatorRepository, IPCBARepository pcbaRepository,
        IDbTransaction dbTransaction)
    {
        _actuatorRepository = actuatorRepository;
        _pcbaRepository = pcbaRepository;
        _dbTransaction = dbTransaction;
    }

    public async Task Handle(CreateOrUpdateActuatorCommand request, CancellationToken cancellationToken)
    {
        var actuatorId = CompositeActuatorId.From(request.WorkOrderNumber, request.SerialNumber);
        var pcba = await _pcbaRepository.GetPCBA(request.PCBAUid);
        try
        {
            var actuator = await _actuatorRepository.GetActuator(actuatorId);
            actuator.UpdatePCBA(pcba);
            await _actuatorRepository.UpdateActuator(actuator);
        }
        catch (KeyNotFoundException _)
        {
            var actuator = Actuator.Create(actuatorId, pcba);
            await _actuatorRepository.CreateActuator(actuator);
        }
        
        await _dbTransaction.CommitAsync(cancellationToken);
    }
}