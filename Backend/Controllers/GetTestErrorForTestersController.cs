using System.ComponentModel.DataAnnotations;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
public class GetTestErrorForTestersController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetTestErrorForTestersController(IQueryBus bus)
    {
        _bus = bus;
    }

    [HttpGet]
    [Route("api/GetTestErrorForTesters")]
    [Tags("Test Result")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTestErrorForTestersResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync(
        [FromQuery][Required] List<string> testers,
        [FromQuery][Required] string timePeriod, //TODO lav string til enum
        CancellationToken cancellationToken)
    {
        // "Past Year",
        // "Past 6 Months",
        // "Past Month",
        // "Past Week",
        // "Today"
        
        // var result = await _bus.Send(query, cancellationToken);
        // return File(result, "text/csv", "actuators.csv");
        return Ok();
    }
}

public class GetTestErrorForTestersResponse
{
    public List<GetTestErrorForTestersTester> Testers { get; set; }
    public string DateFormat { get; set; }
}

public class GetTestErrorForTestersTester
{
    public string Name { get; set; }
    public List<GetTestErrorForTestersError> Errors { get; set; }
}

public class GetTestErrorForTestersError
{
    public DateTime Date { get; set; }
    public int ErrorCount { get; set; }
}