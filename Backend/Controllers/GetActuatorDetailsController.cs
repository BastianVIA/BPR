﻿using Application.GetActuatorDetails;
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
    public async Task<IActionResult> GetAsync(int WONo, int SerialNo, CancellationToken cancellationToken)
    {
        var query = GetActuatorDetailsQuery.Create(WONo, SerialNo);
        var result = await _bus.Send(query, cancellationToken);
        return Ok(GetActuatorDetailsResponse.From(result));
    }
    
    public class GetActuatorDetailsResponse
    {
        public int PCBAUid { get; }
        public GetActuatorDetailsResponse(int pcbaUid)
        {
            PCBAUid = pcbaUid;
        }

        internal static GetActuatorDetailsResponse From(GetActuatorDetailsDto result)
        {
            return new GetActuatorDetailsResponse(result.PCBAUId);
        }
    }
}