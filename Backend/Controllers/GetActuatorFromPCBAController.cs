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
    [Route("api/GetActuatorFromPCBA/{uid}/{manufacturerNo?}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActuatorFromPCBAResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync(string uid, int? manufacturerNo, CancellationToken cancellationToken)
    {
        //var query = ;
        //var result = await _bus.Send(query, cancellationToken);
        return Ok(GetActuatorFromPCBAResponse.From());
    }

}

public class GetActuatorFromPCBAResponse
{
    public int WorkOrderNumber { get; }
    public int SerialNumber { get; }
    public string Uid { get; }
    public int ManufacturerNumber { get; }
    
    private GetActuatorFromPCBAResponse(){}

    private GetActuatorFromPCBAResponse(int woNo, int serialNo, string uid, int manufacturerNo)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        Uid = uid;
        ManufacturerNumber = manufacturerNo;
    }

    internal static GetActuatorFromPCBAResponse From()
    {
        return new GetActuatorFromPCBAResponse();
    }
}