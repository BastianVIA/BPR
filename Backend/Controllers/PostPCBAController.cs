using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Application.CreatePCBA;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]

public class PostPCBAController : ControllerBase
{
    private readonly ICommandBus _bus;

    public PostPCBAController(ICommandBus bus)
    {
        _bus = bus;
    }
    
    [HttpPost()]
    [Route("api/[controller]")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] PostPCBARequest request, CancellationToken cancellationToken)
    {
        var cmd = CreatePCBACommand.Create(request.PCBAUid, request.ManufacturerNumber);
        await _bus.Send(cmd, cancellationToken);
        return Ok();
    }

    public class PostPCBARequest
    {
        public int PCBAUid { get; set; }
        public int? ManufacturerNumber { get; set; }
    }

}