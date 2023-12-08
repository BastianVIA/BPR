using TestResult.Domain.Entities;

namespace TestResult.Application.GetStartUpAmounts;

public class GetTestStartUpAmountsDto
{
    public int TestResultAmount { get; set; }
    public int TestErrorAmount { get; set; }
    public int TestResultWithErrorAmount { get; set; }
    public int TestResultWithoutErrorAmount { get; set; }
    
    private GetTestStartUpAmountsDto(){}
    
    private GetTestStartUpAmountsDto(int testResultAmount, int testErrorAmount, int testResultWithErrorAmount, int testResultWithoutErrorAmount)
    {
        TestResultAmount = testResultAmount;
        TestErrorAmount = testErrorAmount;
        TestResultWithErrorAmount = testResultWithErrorAmount;
        TestResultWithoutErrorAmount = testResultWithoutErrorAmount;
    }

    internal static GetTestStartUpAmountsDto From(TestStartUpAmount startUpAmount)
    {
        return new GetTestStartUpAmountsDto(startUpAmount.TestResultAmount, startUpAmount.TestErrorAmount,
            startUpAmount.TestResultWithErrorAmount, startUpAmount.TestResultWithoutErrorAmount);
    }
}