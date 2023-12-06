using BuildingBlocks.Application;
using Domain.Repositories;

namespace Application.GetPCBAChangesForActuator;

public class
    GetPCBAChangesForActuatorQueryHandler : IQueryHandler<GetPCBAChangesForActuatorQuery, GetPCBAChangesForActuatorDto>
{
    private readonly IActuatorPCBAHistoryRepository actuatorPcbaHistoryRepository;

    public GetPCBAChangesForActuatorQueryHandler(IActuatorPCBAHistoryRepository actuatorPcbaHistoryRepository)
    {
        this.actuatorPcbaHistoryRepository = actuatorPcbaHistoryRepository;
    }

    public async Task<GetPCBAChangesForActuatorDto> Handle(GetPCBAChangesForActuatorQuery request,
        CancellationToken cancellationToken)
    {
        var pcbaChanges =
            await actuatorPcbaHistoryRepository.GetPCBAChangesForActuator(request.WorkOrderNumber, request.SerialNumber);
        return GetPCBAChangesForActuatorDto.From(pcbaChanges);
    }
}