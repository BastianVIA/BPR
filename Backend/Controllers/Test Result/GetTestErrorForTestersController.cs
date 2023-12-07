using System.ComponentModel.DataAnnotations;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;
using TestResult.Application.GetTestErrorForTesters;

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
    [Route("api/[controller]")]
    [Tags("Test Result")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTestErrorForTestersResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync(
        [FromQuery] [Required] List<string> testers,
        [FromQuery] [Required] TesterTimePeriodEnum timePeriod,
        CancellationToken cancellationToken)
    {
        var query = GetTestErrorForTestersQuery.Create(testers, timePeriod);
        var result = await _bus.Send(query, cancellationToken);
        return Ok(GetTestErrorForTestersResponse.From(result));
    }
}

public class GetTestErrorForTestersResponse
{
    public List<GetTestErrorForTestersTester> ErrorsForTesters { get;  }

    private GetTestErrorForTestersResponse() { }
    private GetTestErrorForTestersResponse(List<GetTestErrorForTestersTester> errorsForTesters)
    {
        ErrorsForTesters = errorsForTesters;
    }

    public static GetTestErrorForTestersResponse From(GetTestErrorForTestersDto dto)
    {
        List<GetTestErrorForTestersTester> testers = new();
        foreach (var dtoTester in dto.ErrorsForTesters)
        {
            testers.Add(GetTestErrorForTestersTester.From(dtoTester.Name, dtoTester.Errors));
        }
        return new GetTestErrorForTestersResponse(testers);
    }
}

public class GetTestErrorForTestersTester
{
    public string Name { get;  }
    public List<GetTestErrorForTestersError> Errors { get;  }
    private GetTestErrorForTestersTester(){} 
    private GetTestErrorForTestersTester(string name, List<GetTestErrorForTestersError> errors)
    {
        Name = name;
        Errors = errors;
    }

    public static GetTestErrorForTestersTester From(string name, List<GetTestErrorForTestersErrorDto> dtos)
    {
        List<GetTestErrorForTestersError> errors = new();
        foreach (var dto in dtos)
        {
            errors.Add(GetTestErrorForTestersError.From(dto.Date, dto.ErrorCount));
        }
        return new GetTestErrorForTestersTester(name, errors);
    }
   
}

public class GetTestErrorForTestersError
{
    public DateTime Date { get; }
    public int ErrorCount { get; }
    private GetTestErrorForTestersError(){}
    private GetTestErrorForTestersError(DateTime date, int errorCount)
    {
        Date = date;
        ErrorCount = errorCount;
    }

    public static GetTestErrorForTestersError From(DateTime date, int errorCount)
    {
        return new GetTestErrorForTestersError(date, errorCount);
    }

}