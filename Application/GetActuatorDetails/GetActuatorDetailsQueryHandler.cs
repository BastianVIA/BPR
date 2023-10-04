using BuildingBlocks.Application;
using Domain.Entities;
using Domain.Repositories;

namespace Application.GetActuatorDetails;

public class GetActuatorDetailsQueryHandler : IQueryHandler<GetActuatorDetailsQuery, GetActuatorDetailsDto>
{
    private readonly IActuatorRepository _actuatorRepository;

    public GetActuatorDetailsQueryHandler(IActuatorRepository actuatorRepository)
    {
        _actuatorRepository = actuatorRepository;
    }

    public async Task<GetActuatorDetailsDto> Handle(GetActuatorDetailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var actuatorId = CompositeActuatorId.From(request.WorkOrderNumber, request.SerialNumber);
            var actuator = await _actuatorRepository.GetActuator(actuatorId);
            return GetActuatorDetailsDto.From(actuator);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}