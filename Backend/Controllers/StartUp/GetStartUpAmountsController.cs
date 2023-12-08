using Application.GetStartUpAmounts;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;
using TestResult.Application.GetStartUpAmounts;

namespace Backend.Controllers.StartUp;

public class GetStartUpAmountsController : ControllerBase
{
    private readonly IQueryBus _bus;

    public GetStartUpAmountsController(IQueryBus bus)
    {
        _bus = bus;
    }
    
    [HttpGet]
    [Route("api/[controller]")]
    [Tags("StartUp")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetStartUpResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var actuatorQuery = GetActuatorStartUpAmountsQuery.Create();
        var actuatorResult = _bus.Send(actuatorQuery, cancellationToken);
        
        var testQuery = GetTestStartUpAmountsQuery.Create();
        var testResult = _bus.Send(testQuery, cancellationToken);
        
        return Ok(GetStartUpResponse.From(await actuatorResult, await testResult));
    }

    public class GetStartUpResponse
    {
        public int ActuatorAmount { get; set; }
        public int TestResultAmount { get; set; }
        public int TestErrorAmount { get; set; }
        public int TestResultWithErrorAmount { get; set; }
        public int TestResultWithoutErrorAmount { get; set; }
        
        private GetStartUpResponse(){}
        
        private GetStartUpResponse( int actuatorAmount, int testResultAmount, int testErrorAmount, 
            int testResultWithErrorAmount, int testResultWithoutErrorAmount)
        {
            ActuatorAmount = actuatorAmount;
            TestResultAmount = testResultAmount;
            TestErrorAmount = testErrorAmount;
            TestResultWithErrorAmount = testResultWithErrorAmount;
            TestResultWithoutErrorAmount = testResultWithoutErrorAmount;
        }
        public static GetStartUpResponse From(GetActuatorStartUpAmountsDto actuatorResult, GetTestStartUpAmountsDto testResult)
        {
            return new GetStartUpResponse(actuatorResult.ActuatorAmount, testResult.TestResultAmount, testResult.TestErrorAmount,
                testResult.TestResultWithErrorAmount, testResult.TestResultWithoutErrorAmount);
        }
    }
}