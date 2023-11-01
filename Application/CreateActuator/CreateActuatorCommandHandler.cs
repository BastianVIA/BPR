using BuildingBlocks.Application;
using Domain.Entities;
using Domain.Repositories;

namespace Application.CreateActuator;

public class CreateActuatorCommandHandler : ICommandHandler<CreateActuatorCommand>
{
    private IActuatorRepository _actuatorRepository;

    public CreateActuatorCommandHandler(IActuatorRepository actuatorRepository)
    {
        _actuatorRepository = actuatorRepository;
    }

    public async Task Handle(CreateActuatorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var actuatorId = CompositeActuatorId.From(request.WorkOrderNumber, request.SerialNumber);
            var actuator = Actuator.Create(actuatorId, request.PCBA);
            await _actuatorRepository.CreateActuator(actuator);
        } catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}