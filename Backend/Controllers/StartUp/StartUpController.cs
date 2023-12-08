using Application.GetStartUpAmounts;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.StartUp;

public class StartUpController : ControllerBase
{
    private readonly IQueryBus _bus;

    public StartUpController(IQueryBus bus)
    {
        _bus = bus;
    }
    
    [HttpGet]
    [Route("api/GetStartUpAmounts")]
    [Tags("StartUp")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetStartUpResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var query = GetStartUpAmountsQuery.Create();
        var result = await _bus.Send(query, cancellationToken);
        return Ok(GetStartUpResponse.From(result));
    }

    public class GetStartUpResponse
    {
        public int ActuatorAmount { get; set; }
        public int TestResultAmount { get; set; }
        public int TestErrorAmount { get; set; }
        public int TestResultWithErrorAmount { get; set; }
        public int TestResultWithoutErrorAmount { get; set; }
        
        private GetStartUpResponse()
        {
        }
        private GetStartUpResponse( int actuatorAmount, int testResultAmount, int testErrorAmount, 
            int testResultWithErrorAmount, int testResultWithoutErrorAmount)
        {
            ActuatorAmount = actuatorAmount;
            TestResultAmount = testResultAmount;
            TestErrorAmount = testErrorAmount;
            TestResultWithErrorAmount = testResultWithErrorAmount;
            TestResultWithoutErrorAmount = testResultWithoutErrorAmount;
        }

        public static GetStartUpResponse From(GetStartUpAmountsDto result)
        {
            return new GetStartUpResponse(result.ActuatorAmount, result.TestResultAmount, result.TestErrorAmount,
                result.TestResultWithErrorAmount, result.TestResultWithoutErrorAmount);
        }
    }
}