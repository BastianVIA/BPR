using BuildingBlocks.Application;
using Domain.Repositories;

namespace Application.GetPCBAChangesForActuator;

public class
    GetPCBAChangesForActuatorQueryHandler : IQueryHandler<GetPCBAChangesForActuatorQuery, GetPCBAChangesForActuatorDto>
{
    private readonly IActuatorPCBAHistory _actuatorPcbaHistory;

    public GetPCBAChangesForActuatorQueryHandler(IActuatorPCBAHistory actuatorPcbaHistory)
    {
        _actuatorPcbaHistory = actuatorPcbaHistory;
    }

    public async Task<GetPCBAChangesForActuatorDto> Handle(GetPCBAChangesForActuatorQuery request,
        CancellationToken cancellationToken)
    {
        var pcbaChanges =
            await _actuatorPcbaHistory.GetPCBAChangesForActuator(request.WorkOrderNumber, request.SerialNumber);
        return GetPCBAChangesForActuatorDto.From(pcbaChanges);
    }
}