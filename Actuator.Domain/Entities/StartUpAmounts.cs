namespace Domain.Entities;

public class StartUpAmounts
{
    public int ActuatorAmount { get; private set; }
    public int TestResultAmount { get; private set; }
    public int TestErrorAmount { get; private set; }
    public int TestResultWithErrorAmount { get; private set; }
    public int TestResultWithoutErrorAmount { get; private set; }
    
    private StartUpAmounts()
    {
    }
    
    public StartUpAmounts(int actuatorAmount, int testResultAmount, int testErrorAmount, int testResultWithErrorAmount, int testResultWithoutErrorAmount)
    {
        ActuatorAmount = actuatorAmount;
        TestResultAmount = testResultAmount;
        TestErrorAmount = testErrorAmount;
        TestResultWithErrorAmount = testResultWithErrorAmount;
        TestResultWithoutErrorAmount = testResultWithoutErrorAmount;
    }

    public static StartUpAmounts Create(int actuatorAmount, int testResultAmount, int testErrorAmount, 
        int testResultWithErrorAmount, int testResultWithoutErrorAmount)
    {
        var startUpAmounts = new StartUpAmounts(actuatorAmount, testResultAmount, testErrorAmount,
            testResultWithErrorAmount, testResultWithoutErrorAmount);
        return startUpAmounts;
    }

    
}