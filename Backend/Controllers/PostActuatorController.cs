using System.Net;
using Application.CreateOrUpdateActuator;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[ApiExplorerSettings(GroupName = "Actuator")]

public class PostActuatorController : ControllerBase
{
    private readonly ICommandBus _bus;

    public PostActuatorController(ICommandBus bus)
    {
        _bus = bus;
    }
    
    [HttpPost()]
    [Route("api/[controller]")]
    [Tags("Actuator")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] PostActuatorRequest request, CancellationToken cancellationToken)
    {
        var cmd = CreateOrUpdateActuatorCommand.Create(request.WorkOrderNumber, request.SerialNumber, request.PCBAUid);
        await _bus.Send(cmd, cancellationToken);
        return Ok();
    }

    public class PostActuatorRequest
    {
        public int WorkOrderNumber { get; set; }
        public int SerialNumber { get; set; }
        public string PCBAUid { get; set; }
    }

}