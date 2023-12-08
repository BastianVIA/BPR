namespace Domain.Entities;

public class ActuatorStartUpAmounts
{
    public int ActuatorAmount { get; private set; }

    private ActuatorStartUpAmounts()
    {
    }
    
    public ActuatorStartUpAmounts(int actuatorAmount)
    {
        ActuatorAmount = actuatorAmount;
    }

    public static ActuatorStartUpAmounts Create(int actuatorAmount)
    {
        var startUpAmounts = new ActuatorStartUpAmounts(actuatorAmount);
        return startUpAmounts;
    }

    
}