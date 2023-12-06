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
        return Ok(GetTestErrorsWithFilterResponse.From(result));
    }
}

public class GetTestErrorsWithFilterResponse
{
    public List<int> PossibleErrorCodes { get; }
    public List<GetTestErrorsWithFilterSingleLine> DataLines { get; }

    public GetTestErrorsWithFilterResponse(List<int> possibleErrorCodes,
        List<GetTestErrorsWithFilterSingleLine> dataLines)
    {
        PossibleErrorCodes = possibleErrorCodes;
        DataLines = dataLines;
    }

    public static GetTestErrorsWithFilterResponse From(GetTestErrorsWithFilterDto dto)
    {
        List<GetTestErrorsWithFilterSingleLine> dataLines = new();
        foreach (var singleLine in dto.DataLines)
        {
            dataLines.Add(GetTestErrorsWithFilterSingleLine.From(singleLine));
        }

        return new GetTestErrorsWithFilterResponse(dto.PossibleErrorCodes, dataLines);
    }
}

public class GetTestErrorsWithFilterSingleLine
{
    public List<GetTestErrorsWithFilterTestData> ListOfErrors { get; }
    public int TotalErrors { get; }
    public int TotalTests { get; }
    public DateTime StartIntervalAsDate { get; }
    public DateTime EndIntervalAsDate { get; }

    private GetTestErrorsWithFilterSingleLine(List<GetTestErrorsWithFilterTestData> listOfErrors, int totalErrors,
        int totalTests,
        DateTime startIntervalAsDate, DateTime endIntervalAsDate)
    {
        ListOfErrors = listOfErrors;
        TotalErrors = totalErrors;
        TotalTests = totalTests;
        StartIntervalAsDate = startIntervalAsDate;
        EndIntervalAsDate = endIntervalAsDate;
    }

    public static GetTestErrorsWithFilterSingleLine From(
        GetTestErrorsWithFilterSingleLineDto singleLineDto)
    {
        List<GetTestErrorsWithFilterTestData> testData = new();
        foreach (var testDataDto in singleLineDto.ListOfErrors)
        {
            testData.Add(GetTestErrorsWithFilterTestData.From(testDataDto.ErrorCode, testDataDto.AmountOfErrors));
        }

        return new GetTestErrorsWithFilterSingleLine(testData, singleLineDto.TotalErrors, singleLineDto.TotalTests,
            singleLineDto.StartIntervalAsDate,
            singleLineDto.EndIntervalAsDate);
    }
}

public class GetTestErrorsWithFilterTestData
{
    public int ErrorCode { get; }
    public int AmountOfErrors { get; }

    private GetTestErrorsWithFilterTestData(int errorCode, int amountOfErrors)
    {
        ErrorCode = errorCode;
        AmountOfErrors = amountOfErrors;
    }

    public static GetTestErrorsWithFilterTestData From(int errorCode, int amountOfErrors)
    {
        return new GetTestErrorsWithFilterTestData(errorCode, amountOfErrors);
    }
}