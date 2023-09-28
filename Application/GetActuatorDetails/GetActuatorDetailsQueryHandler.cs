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
        var actuatorId = CompositeActuatorId.From(request.WONo, request.SerialNo);
        var actuator = await _actuatorRepository.GetActuator(actuatorId);
        return GetActuatorDetailsDto.From(actuator);
    }
}