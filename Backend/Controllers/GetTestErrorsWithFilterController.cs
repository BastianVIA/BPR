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
    public List<GetTestErrorsWithFilterErrorCodeAndAmount> ListOfErrors { get; }
    public int TotalErrors { get; }
    public int TotalTests { get; }
    public DateTime StartIntervalAsDate { get; }
    public DateTime EndIntervalAsDate { get; }

    private GetTestErrorsWithFilterSingleLine(List<GetTestErrorsWithFilterErrorCodeAndAmount> listOfErrors, int totalErrors,
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
        List<GetTestErrorsWithFilterErrorCodeAndAmount> testData = new();
        foreach (var testDataDto in singleLineDto.ListOfErrors)
        {
            testData.Add(GetTestErrorsWithFilterErrorCodeAndAmount.From(testDataDto.ErrorCode, testDataDto.ErrorMessage,
                testDataDto.AmountOfErrors));
        }

        return new GetTestErrorsWithFilterSingleLine(testData, singleLineDto.TotalErrors, singleLineDto.TotalTests,
            singleLineDto.StartIntervalAsDate,
            singleLineDto.EndIntervalAsDate);
    }
}

public class GetTestErrorsWithFilterErrorCodeAndAmount
{
    public int ErrorCode { get; }
    public string ErrorMessage { get; }
    public int AmountOfErrors { get; }

    private GetTestErrorsWithFilterErrorCodeAndAmount(int errorCode, string errorMessage, int amountOfErrors)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        AmountOfErrors = amountOfErrors;
    }

    public static GetTestErrorsWithFilterErrorCodeAndAmount From(int errorCode, string errorMessage, int amountOfErrors)
    {
        return new GetTestErrorsWithFilterErrorCodeAndAmount(errorCode, errorMessage, amountOfErrors);
    }
}