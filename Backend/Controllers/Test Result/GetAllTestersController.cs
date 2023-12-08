using BuildingBlocks.Application;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TestResult.Application.GetAllTesters;

namespace Backend.Controllers.Test_Result;

[ApiController]

public class GetAllTestersController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetAllTestersController(IQueryBus bus)
    {
        _bus = bus;
    }
    
    [HttpGet]
    [Route("api/[controller]")]
    [Tags("Test Result")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllTestersResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var query = GetAllTestersQuery.Create();
        var result = await _bus.Send(query, cancellationToken);
        return Ok(GetAllTestersResponse.From(result));
    }
}

public class GetAllTestersResponse
{
    public List<string> AllTesters { get; }
    
    private GetAllTestersResponse(){}

    private GetAllTestersResponse(List<string> allTesters)
    {
        AllTesters = allTesters;
    }

    public static GetAllTestersResponse From(GetAllTestersDto dto)
    {
        return new GetAllTestersResponse(dto.AllTesters);
    }
}