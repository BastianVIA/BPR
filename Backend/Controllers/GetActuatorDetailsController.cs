using Application.GetActuatorDetails;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]

public class GetActuatorDetailsController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetActuatorDetailsController(IQueryBus bus)
    {
        _bus = bus;
    }

    [HttpGet()]
    [Route("api/GetActuatorDetails/{WONo}/{SerialNo}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActuatorDetailsResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(string WONo, string SerialNo, CancellationToken cancellationToken)
    {
        var query = GetActuatorDetailsQuery.Create(WONo, SerialNo);
        var result = await _bus.Send(query, cancellationToken);
        return Ok(GetActuatorDetailsResponse.From(result));
    }
    
    public class GetActuatorDetailsResponse
    {

        public string PCBAId { get; }
        public GetActuatorDetailsResponse(string PCBAId)
        {
            this.PCBAId = PCBAId;
        }

        internal static GetActuatorDetailsResponse From(GetActuatorDetailsDto result)
        {
            return new GetActuatorDetailsResponse(result.PCBAId);
        }
    }
}