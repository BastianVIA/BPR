using Application.GetPCBAChangesForActuator;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
public class GetPCBAChangesForActuatorController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetPCBAChangesForActuatorController(IQueryBus bus)
    {
        _bus = bus;
    }

    [HttpGet()]
    [Route("api/[controller]/{woNo}/{serialNo}")]
    [Tags("Actuator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPCBAChangesForActuatorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync(int woNo, int serialNo, CancellationToken cancellationToken)
    {
        var query = GetPCBAChangesForActuatorQuery.Create(woNo, serialNo);
        var result = await _bus.Send(query, cancellationToken);
        return Ok(GetPCBAChangesForActuatorResponse.From(result));
    }
}

public class GetPCBAChangesForActuatorResponse
{
    public List<GetPCBAChangesForActuatorChange> Changes { get; }

    private GetPCBAChangesForActuatorResponse()
    {
    }

    public GetPCBAChangesForActuatorResponse(List<GetPCBAChangesForActuatorChange> changes)
    {
        Changes = changes;
    }

    internal static GetPCBAChangesForActuatorResponse From(GetPCBAChangesForActuatorDto pcbaChanges)
    {
        List<GetPCBAChangesForActuatorChange> changes = new();
        foreach (var change in pcbaChanges.Changes)
        {
            changes.Add(GetPCBAChangesForActuatorChange.From(change));
        }

        return new GetPCBAChangesForActuatorResponse(changes);
    }
}

public class GetPCBAChangesForActuatorChange
{
    public int WorkOrderNumber { get; }
    public int SerialNumber { get; }
    public string OldPCBAUid { get; }
    public DateTime RemovalTime { get; }

    private GetPCBAChangesForActuatorChange()
    {
    }

    private GetPCBAChangesForActuatorChange(int woNo, int serialNo, string oldPCBAUid, DateTime removalTime)
    {
        OldPCBAUid = oldPCBAUid;
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        RemovalTime = removalTime;
    }

    internal static GetPCBAChangesForActuatorChange From(GetPCBAChangesForActuatorChangeDto change)
    {
        return new GetPCBAChangesForActuatorChange(change.WorkOrderNumber, change.SerialNumber, change.OldPCBAUid,
            change.RemovalTime);
    }
}