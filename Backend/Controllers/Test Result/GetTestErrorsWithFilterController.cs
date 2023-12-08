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
    [Route("api/[controller]")]
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
    public List<GetTestErrorsWithFilterErrorCodeAndMessage> PossibleErrorCodes { get; }
    public List<GetTestErrorsWithFilterSingleLine> DataLines { get; }

    public GetTestErrorsWithFilterResponse(List<GetTestErrorsWithFilterErrorCodeAndMessage> possibleErrorCodes,
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

        List<GetTestErrorsWithFilterErrorCodeAndMessage> possibleErrorCodes = dto.PossibleErrorCodes.Select(code =>
            GetTestErrorsWithFilterErrorCodeAndMessage.From(code.ErrorCode, code.ErrorMessage)).ToList();

        return new GetTestErrorsWithFilterResponse(possibleErrorCodes, dataLines);
    }
}

public class GetTestErrorsWithFilterErrorCodeAndMessage
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }

    private GetTestErrorsWithFilterErrorCodeAndMessage()
    {
    }

    private GetTestErrorsWithFilterErrorCodeAndMessage(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }

    public static GetTestErrorsWithFilterErrorCodeAndMessage From(int errorCode, string errorMessage)
    {
        return new GetTestErrorsWithFilterErrorCodeAndMessage(errorCode, errorMessage);
    }
}

public class GetTestErrorsWithFilterSingleLine
{
    public List<GetTestErrorsWithFilterErrorCodeAndAmount> ListOfErrors { get; }
    public int TotalErrors { get; }
    public int TotalTests { get; }
    public DateTime StartIntervalAsDate { get; }
    public DateTime EndIntervalAsDate { get; }

    private GetTestErrorsWithFilterSingleLine(List<GetTestErrorsWithFilterErrorCodeAndAmount> listOfErrors,
        int totalErrors,
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
            testData.Add(GetTestErrorsWithFilterErrorCodeAndAmount.From(testDataDto.ErrorCode,
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
    public int AmountOfErrors { get; }

    private GetTestErrorsWithFilterErrorCodeAndAmount(int errorCode, int amountOfErrors)
    {
        ErrorCode = errorCode;
        AmountOfErrors = amountOfErrors;
    }

    public static GetTestErrorsWithFilterErrorCodeAndAmount From(int errorCode, int amountOfErrors)
    {
        return new GetTestErrorsWithFilterErrorCodeAndAmount(errorCode, amountOfErrors);
    }
}