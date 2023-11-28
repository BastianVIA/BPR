using Application.GetActuatorFromPCBA;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]

public class GetActuatorFromPCBAController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetActuatorFromPCBAController(IQueryBus bus)
    {
        _bus = bus;
    }
    
    [HttpGet()]
    [Route("api/GetActuatorFromPCBA/{uid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetActuatorFromPCBAActuator>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync([FromRoute] string uid,[FromQuery] int? manufacturerNo, CancellationToken cancellationToken)
    {
        var query = GetActuatorFromPCBAQuery.Create(uid, manufacturerNo);
        var result = await _bus.Send(query, cancellationToken);
        List<GetActuatorFromPCBAActuator> toReturn = new();
        foreach (var resultActuator in result.Actuators)
        {
            toReturn.Add(GetActuatorFromPCBAActuator.From(resultActuator));
        }
        return Ok(toReturn);
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
