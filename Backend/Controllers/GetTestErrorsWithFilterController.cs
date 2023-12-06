using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;
using TestResult.Application.GetTestErrorsWithFilter;

namespace Backend.Controllers;

[ApiController]
public class GetTestErrorsWithFilterController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetTestErrorsWithFilterController(IQueryBus bus)
    {
        _bus = bus;
    }

    [HttpGet()]
    [Route("api/GetTestErrorsWithFilter")]
    [Tags("Test Result")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTestErrorsWithFilterResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync(
        [FromQuery] GetTestErrorsWithFilterQuery query,
        CancellationToken cancellationToken)
    {
        query.Validate();
        var result = await _bus.Send(query, cancellationToken);
        return Ok(new GetTestErrorsWithFilterResponse());
    }
}

public class GetTestErrorsWithFilterResponse
{
    public List<int> PossibleErrorCodes { get; }
    public List<GetTestErrorsWithFilterSingleLine> DataLines { get; }

    public GetTestErrorsWithFilterResponse()
    {
    }
}

public class GetTestErrorsWithFilterSingleLine
{
    public DateTime StartIntervalAsDate { get; }
    public DateTime EndIntervalAsDate { get; }
    public int TotalErrors { get; }
    public List<GetTestErrorsWithFilterTestData> listOfErrors { get; }
}

public class GetTestErrorsWithFilterTestData
{
    public int ErroCode { get; }
    public int AmountOfErrors { get; }
}