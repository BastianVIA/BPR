using BuildingBlocks.Application;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Application.GetActuatorFromPCBA;

public class GetActuatorFromPCBAQueryHandler : IQueryHandler<GetActuatorFromPCBAQuery, GetActuatorFromPCBADto>
{
    private readonly IActuatorRepository _actuatorRepository;

    public GetActuatorFromPCBAQueryHandler(IActuatorRepository actuatorRepository)
    {
        _actuatorRepository = actuatorRepository;
    }

    public async Task<GetActuatorFromPCBADto> Handle(GetActuatorFromPCBAQuery request, CancellationToken cancellationToken)
    {
        try
        {
            List<Actuator> actuators = await _actuatorRepository.GetActuatorsFromPCBAAsync(request.Uid, request.ManufacturerNo);
            return GetActuatorFromPCBADto.From(actuators);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}