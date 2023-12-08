using Domain.Entities;

namespace Application.GetStartUpAmounts;

public class GetStartUpAmountsDto
{
    public int ActuatorAmount { get; set; }
    public int TestResultAmount { get; set; }
    public int TestErrorAmount { get; set; }
    public int TestResultWithErrorAmount { get; set; }
    public int TestResultWithoutErrorAmount { get; set; }
    
    private GetStartUpAmountsDto()
    {
    }
    
    private GetStartUpAmountsDto(int actuatorAmount, int testResultAmount, int testErrorAmount, int testResultWithErrorAmount, int testResultWithoutErrorAmount)
    {
        ActuatorAmount = actuatorAmount;
        TestResultAmount = testResultAmount;
        TestErrorAmount = testErrorAmount;
        TestResultWithErrorAmount = testResultWithErrorAmount;
        TestResultWithoutErrorAmount = testResultWithoutErrorAmount;
    }

    internal static GetStartUpAmountsDto From(StartUpAmounts startUpAmounts)
    {
        return new GetStartUpAmountsDto(startUpAmounts.ActuatorAmount, startUpAmounts.TestResultAmount,
            startUpAmounts.TestErrorAmount, startUpAmounts.TestResultWithErrorAmount,
            startUpAmounts.TestResultWithoutErrorAmount);
    }
}