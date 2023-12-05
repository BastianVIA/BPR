using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;
using TestResult.Application.GetActuatorTestDetails;

namespace Backend.Controllers;

[ApiController]
public class GetActuatorTestDetails : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetActuatorTestDetails(IQueryBus bus)
    {
        _bus = bus;
    }

    private GetActuatorTestDetails()
    {
    }

    [HttpGet()]
    [Route("api/GetActuatorTestDetails/{woNo}/{serialNo}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActuatorTestDetailsResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync(int woNo, int serialNo, CancellationToken cancellationToken)
    {
        var query = GetActuatorTestDetailsQuery.Create(woNo, serialNo);
        var result = await _bus.Send(query, cancellationToken);
        return Ok(GetActuatorTestDetailsResponse.From(result));
    }

    public class GetActuatorTestDetailsResponse
    {
        public int WorkOrderNumber { get; private set; }
        public int SerialNumber { get; private set; }
        public string Tester { get; set; }
        public int Bay { get; set; }
        public string? MinServoPosition { get; set; }
        public string? MaxServoPosition { get; set; }
        public string? MinBuslinkPosition { get; set; }
        public string? MaxBuslinkPosition { get; set; }
        public string? ServoStroke { get; set; }

        private GetActuatorTestDetailsResponse(int workOrderNumber, int serialNumber, string tester, int bay,
            string? minServoPosition, string? maxServoPosition, string? minBuslinkPosition, string? maxBuslinkPosition,
            string? servoStroke)
        {
            WorkOrderNumber = workOrderNumber;
            SerialNumber = serialNumber;
            Tester = tester;
            Bay = bay;
            MinServoPosition = minServoPosition;
            MaxServoPosition = maxServoPosition;
            MinBuslinkPosition = minBuslinkPosition;
            MaxBuslinkPosition = maxBuslinkPosition;
            ServoStroke = servoStroke;
        }

        internal static GetActuatorTestDetailsResponse From(GetActuatorTestDetailsDto result)
        {
            return new GetActuatorTestDetailsResponse(result.WorkOrderNumber, result.SerialNumber, result.Tester,
                result.Bay, result.MinServoPosition, result.MaxServoPosition, result.MinBuslinkPosition,
                result.MaxServoPosition, result.ServoStroke);
        }
    }
}