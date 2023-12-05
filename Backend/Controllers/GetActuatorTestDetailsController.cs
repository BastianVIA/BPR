using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;
using TestResult.Application.GetActuatorTestDetails;

namespace Backend.Controllers;

[ApiController]
public class GetActuatorTestDetailsController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetActuatorTestDetailsController(IQueryBus bus)
    {
        _bus = bus;
    }

    [HttpGet()]
    [Route("api/GetActuatorTestDetails")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetActuatorTestDetailsResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync(
        [FromQuery] int? woNo,
        [FromQuery] int? serialNo,
        [FromQuery] string? tester,
        [FromQuery] int? bay,
        CancellationToken cancellationToken)
    {
        var query = GetActuatorTestDetailsQuery.Create(woNo, serialNo, tester, bay);
        var result = await _bus.Send(query, cancellationToken);
        return Ok(GetActuatorTestDetailsResponse.From(result));
    }

    public class GetActuatorTestDetailsResponse
    {
        public List<GetActuatorTestActuator> ActuatorTest { get; private set; }

        private GetActuatorTestDetailsResponse()
        {
        }

        private GetActuatorTestDetailsResponse(List<GetActuatorTestActuator> actuatorTest)
        {
            ActuatorTest = actuatorTest;
        }

        internal static GetActuatorTestDetailsResponse From(GetActuatorTestDetailsDto result)
        {
            List<GetActuatorTestActuator> actuatorTests = new List<GetActuatorTestActuator>();
            foreach (var actuatorTest in result.ActuatorTestDetailDtos)
            {
                actuatorTests.Add(GetActuatorTestActuator.From(actuatorTest));
            }

            return new GetActuatorTestDetailsResponse(actuatorTests);
        }

        public class GetActuatorTestActuator
        {
            public int? WorkOrderNumber { get; private set; }
            public int? SerialNumber { get; private set; }
            public string? Tester { get; set; }
            public int? Bay { get; set; }
            public string? MinServoPosition { get; set; }
            public string? MaxServoPosition { get; set; }
            public string? MinBuslinkPosition { get; set; }
            public string? MaxBuslinkPosition { get; set; }
            public string? ServoStroke { get; set; }

            internal static GetActuatorTestActuator From(ActuatorTestDetailDTO result)
            {
                return new GetActuatorTestActuator
                {
                    WorkOrderNumber = result.WorkOrderNumber,
                    SerialNumber = result.SerialNumber,
                    Tester = result.Tester,
                    Bay = result.Bay,
                    MinServoPosition = result.MinServoPosition,
                    MaxServoPosition = result.MaxServoPosition,
                    MinBuslinkPosition = result.MinBuslinkPosition,
                    MaxBuslinkPosition = result.MaxBuslinkPosition,
                    ServoStroke = result.ServoStroke
                };
            }
        }
    }
}