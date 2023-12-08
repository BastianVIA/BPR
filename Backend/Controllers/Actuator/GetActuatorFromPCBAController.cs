using Application.GetActuatorFromPCBA;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Actuator;

[ApiController]
public class GetActuatorFromPCBAController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetActuatorFromPCBAController(IQueryBus bus)
    {
        _bus = bus;
    }

    [HttpGet()]
    [Route("api/[controller]/{uid}")]
    [Tags("Actuator")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActuatorFromPCBAResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync([FromRoute] string uid, [FromQuery] int? manufacturerNo,
        CancellationToken cancellationToken)
    {
        var query = GetActuatorFromPCBAQuery.Create(uid, manufacturerNo);
        var result = await _bus.Send(query, cancellationToken);
        return Ok(GetActuatorFromPCBAResponse.From(result));
    }
}

public class GetActuatorFromPCBAResponse
{
    public List<GetActuatorFromPCBAActuator> Actuators { get; }

    private GetActuatorFromPCBAResponse() { }

    private GetActuatorFromPCBAResponse(List<GetActuatorFromPCBAActuator> actuators)
    {
        Actuators = actuators;
    }

    internal static GetActuatorFromPCBAResponse From(GetActuatorFromPCBADto dto)
    {
        List<GetActuatorFromPCBAActuator> actuators = new();
        foreach (var dtoActuator in dto.Actuators)
        {
            actuators.Add(GetActuatorFromPCBAActuator.From(dtoActuator));
        }

        return new GetActuatorFromPCBAResponse(actuators);
    }
}

public class GetActuatorFromPCBAActuator
{
    public int WorkOrderNumber { get; }
    public int SerialNumber { get; }
    public string Uid { get; }
    public int ManufacturerNumber { get; }

    private GetActuatorFromPCBAActuator() { }

    private GetActuatorFromPCBAActuator(int woNo, int serialNo, string uid, int manufacturerNo)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        Uid = uid;
        ManufacturerNumber = manufacturerNo;
    }

    internal static GetActuatorFromPCBAActuator From(GetActuatorFromPCBAActuatordto actuator)
    {
        return new GetActuatorFromPCBAActuator(
            actuator.WorkOrderNumber,
            actuator.SerialNumber,
            actuator.Uid,
            actuator.ManufacturerNumber
        );
    }
}