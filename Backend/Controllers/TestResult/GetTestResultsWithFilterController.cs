using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;
using TestResult.Application.GetTestResultsWithFilter;

namespace Backend.Controllers.TestResult;

[ApiController]
public class GetTestResultsWithFilterController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetTestResultsWithFilterController(IQueryBus bus)
    {
        _bus = bus;
    }

    [HttpGet()]
    [Route("api/[controller]")]
    [Tags("Test Result")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTestResultsWithFilterResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync([FromQuery] GetTestResultsWithFilterQuery query,
        CancellationToken cancellationToken)
    {
        query.Validate();
        var result = await _bus.Send(query, cancellationToken);
        return Ok(GetTestResultsWithFilterResponse.From(result));
    }
}

public class GetTestResultsWithFilterResponse
{
    public List<GetTestResultWithFilterActuator> ActuatorTest { get; private set; }

    private GetTestResultsWithFilterResponse()
    {
    }

    private GetTestResultsWithFilterResponse(List<GetTestResultWithFilterActuator> actuatorTest)
    {
        ActuatorTest = actuatorTest;
    }

    internal static GetTestResultsWithFilterResponse From(GetTestResultsWithFilterDto result)
    {
        List<GetTestResultWithFilterActuator> actuatorTests = new List<GetTestResultWithFilterActuator>();
        foreach (var actuatorTest in result.TestResultDtos)
        {
            actuatorTests.Add(GetTestResultWithFilterActuator.From(actuatorTest));
        }

        return new GetTestResultsWithFilterResponse(actuatorTests);
    }
}

public class GetTestResultWithFilterActuator
{
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
    public string Tester { get; set; }
    public int Bay { get; set; }
    public string MinServoPosition { get; set; }
    public string MaxServoPosition { get; set; }
    public string MinBuslinkPosition { get; set; }
    public string MaxBuslinkPosition { get; set; }
    public string ServoStroke { get; set; }
    public DateTime TimeOccured { get; set; }
    public List<GetTestResultsWithFilterTestError> TestErrors { get; set; }

    internal static GetTestResultWithFilterActuator From(TestResultsWithFilterDTO result)
    {
        var errors = result.TestErrors.Select(error => new GetTestResultsWithFilterTestError
            {
                Tester = error.Tester,
                Bay = error.Bay,
                ErrorCode = error.ErrorCode,
                ErrorMessage = error.ErrorMessage,
                TimeOccured = error.TimeOccured
            })
            .ToList();
        return new GetTestResultWithFilterActuator
        {
            WorkOrderNumber = result.WorkOrderNumber,
            SerialNumber = result.SerialNumber,
            Tester = result.Tester,
            Bay = result.Bay,
            MinServoPosition = result.MinServoPosition,
            MaxServoPosition = result.MaxServoPosition,
            MinBuslinkPosition = result.MinBuslinkPosition,
            MaxBuslinkPosition = result.MaxBuslinkPosition,
            ServoStroke = result.ServoStroke,
            TimeOccured = result.TimeOccured,
            TestErrors = errors
        };
    }
}

public class GetTestResultsWithFilterTestError
{
    public string Tester { get; set; }
    public int Bay { get; set; }
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime TimeOccured { get; set; }
}