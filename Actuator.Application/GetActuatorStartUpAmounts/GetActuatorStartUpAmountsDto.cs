using Domain.Entities;

namespace Application.GetStartUpAmounts;

public class GetActuatorStartUpAmountsDto
{
    public int ActuatorAmount { get; set; }
    
    private GetActuatorStartUpAmountsDto()
    {
    }
    
    private GetActuatorStartUpAmountsDto(int actuatorAmount)
    {
        ActuatorAmount = actuatorAmount;
    }

    internal static GetActuatorStartUpAmountsDto From(ActuatorStartUpAmounts actuatorStartUpAmounts)
    {
        return new GetActuatorStartUpAmountsDto(actuatorStartUpAmounts.ActuatorAmount);
    }
}