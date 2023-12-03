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
        var cmd = CreatePCBACommand.Create(request.Uid, request.ManufacturerNumber, request.ItemNumber, request.Software, request.ProductionDateCode);
        await _bus.Send(cmd, cancellationToken);
        return Ok();
    }

    public class PostPCBARequest
    {
        public string Uid { get; set; }
        public int ManufacturerNumber { get; set; }
        public string ItemNumber { get; set; }
        public string Software { get; set; }
        public int ProductionDateCode { get; set; }
    }

}