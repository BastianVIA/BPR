namespace TestResult.Domain.Entities;

public class TestStartUpAmount
{
    public int TestResultAmount { get; private set; }
    public int TestErrorAmount { get; private set; }
    public int TestResultWithErrorAmount { get; private set; }
    public int TestResultWithoutErrorAmount { get; private set; }
    public TestStartUpAmount(int testResultAmount, int testErrorAmount, int testResultWithErrorAmount, int testResultWithoutErrorAmount)
    {
        TestResultAmount = testResultAmount;
        TestErrorAmount = testErrorAmount;
        TestResultWithErrorAmount = testResultWithErrorAmount;
        TestResultWithoutErrorAmount = testResultWithoutErrorAmount;
    }

    public static TestStartUpAmount Create(int testResultAmount, int testErrorAmount, int testResultWithErrorAmount, int testResultWithoutErrorAmount)
    {
        return new TestStartUpAmount(testResultAmount, testErrorAmount, testResultWithErrorAmount,
            testResultWithoutErrorAmount);
    }

    
}