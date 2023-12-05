using System.Net;
using Application.CreateOrUpdateActuator;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]

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
        var cmd = CreateOrUpdateActuatorCommand.Create(request.WorkOrderNumber, request.SerialNumber, request.PCBAUid,
            request.ArticleNumber, request.ArticleName, request.CommunicationProtocol, request.CreatedTime);
        await _bus.Send(cmd, cancellationToken);
        return Ok();
    }

    public class PostActuatorRequest
    {
        public int WorkOrderNumber { get; set; }
        public int SerialNumber { get; set; }
        public string PCBAUid { get; set; }
        public string CommunicationProtocol { get; set; }
        public string ArticleNumber { get; set; }
        public string ArticleName { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}