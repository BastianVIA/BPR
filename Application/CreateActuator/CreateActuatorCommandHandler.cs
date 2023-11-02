using BuildingBlocks.Application;
using Domain.Entities;
using Domain.Repositories;

namespace Application.CreateActuator;

public class CreateActuatorCommandHandler : ICommandHandler<CreateActuatorCommand>
{
    private IActuatorRepository _actuatorRepository;
    private IPCBARepository _pcbaRepository;

    public CreateActuatorCommandHandler(IActuatorRepository actuatorRepository, IPCBARepository pcbaRepository)
    {
        _actuatorRepository = actuatorRepository;
        _pcbaRepository = pcbaRepository;
    }

    public async Task Handle(CreateActuatorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var actuatorId = CompositeActuatorId.From(request.WorkOrderNumber, request.SerialNumber);
            var pcba = await _pcbaRepository.GetPCBA(request.PCBAUid);
            var actuator = Actuator.Create(actuatorId, pcba);
            await _actuatorRepository.CreateActuator(actuator);
        }catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}