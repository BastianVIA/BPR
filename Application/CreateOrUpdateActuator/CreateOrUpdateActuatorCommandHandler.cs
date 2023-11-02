using Application.CreateActuator;
using BuildingBlocks.Application;
using BuildingBlocks.Exceptions;
using Domain.Entities;
using Domain.Repositories;

namespace Application.CreateOrUpdateActuator;

public class CreateOrUpdateActuatorCommandHandler : ICommandHandler<CreateOrUpdateActuatorCommand>
{
    private IActuatorRepository _actuatorRepository;

    public CreateOrUpdateActuatorCommandHandler(IActuatorRepository actuatorRepository)
    {
        _actuatorRepository = actuatorRepository;
    }

    public async Task Handle(CreateOrUpdateActuatorCommand request, CancellationToken cancellationToken)
    {
        var actuatorId = CompositeActuatorId.From(request.WorkOrderNumber, request.SerialNumber);

        try
        {
            var actuator = Actuator.Create(actuatorId, request.PCBAUid);
            await _actuatorRepository.CreateActuator(actuator);
        }
        catch (AlreadyExistingException e)
        {
            var actuator = await _actuatorRepository.GetActuator(actuatorId);
            actuator.UpdatePCBAUid(request.PCBAUid);
            await _actuatorRepository.UpdateActuator(actuator);
        }

    }
}