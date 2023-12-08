using System.Net;
using Application.NewPCBAInActuator;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Actuator;

[ApiController]

public class PutNewPCBAInActuator : ControllerBase
{
    private readonly ICommandBus _bus;


    public PutNewPCBAInActuator(ICommandBus bus)
    {
        _bus = bus;
    }
    
    [HttpPut()]
    [Route("api/[controller]")]
    [Tags("Actuator")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync([FromBody] PutActuatorPCBARequest request, CancellationToken cancellationToken)
    {
        var cmd = NewPCBAInActuatorCommand.Create(request.WorkOrderNumber, request.SerialNumber, request.PCBAUid);
        await _bus.Send(cmd, cancellationToken);
        return NoContent();
    }
}

public class PutActuatorPCBARequest
{
    public int WorkOrderNumber { get; set; }
    public int SerialNumber { get; set; }
    public string PCBAUid { get; set; }
}