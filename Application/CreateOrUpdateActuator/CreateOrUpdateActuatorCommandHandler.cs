using BuildingBlocks.Application;
using BuildingBlocks.Exceptions;
using Domain.Entities;
using Domain.Repositories;

namespace Application.CreateOrUpdateActuator;

public class CreateOrUpdateActuatorCommandHandler : ICommandHandler<CreateOrUpdateActuatorCommand>
{
    private IActuatorRepository _actuatorRepository;
    private IPCBARepository _pcbaRepository;

    public CreateOrUpdateActuatorCommandHandler(IActuatorRepository actuatorRepository, IPCBARepository pcbaRepository)
    {
        _actuatorRepository = actuatorRepository;
        _pcbaRepository = pcbaRepository;
    }

    public async Task Handle(CreateOrUpdateActuatorCommand request, CancellationToken cancellationToken)
    {
        var actuatorId = CompositeActuatorId.From(request.WorkOrderNumber, request.SerialNumber);

        try
        {
            var pcba = await FindOrCreatePCBA(request.PCBAUid);
            var actuator = Actuator.Create(actuatorId, pcba);
            await _actuatorRepository.CreateActuator(actuator);
        }
        catch (AlreadyExistingException e)
        {
            var pcba = await FindOrCreatePCBA(request.PCBAUid);
            var actuator = await _actuatorRepository.GetActuator(actuatorId);
            actuator.UpdatePCBA(pcba);
            await _actuatorRepository.UpdateActuator(actuator);
        }
    }

    private async Task<PCBA> FindOrCreatePCBA(string pcbaUid)
    {
        var pcba = new PCBA(pcbaUid, 0);
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