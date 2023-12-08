using BuildingBlocks.Application;

namespace Application.GetStartUpAmounts;

public class GetActuatorStartUpAmountsQuery : IQuery<GetActuatorStartUpAmountsDto>
{
    private GetActuatorStartUpAmountsQuery()
    {
    }

    public static GetActuatorStartUpAmountsQuery Create()
    {
        return new GetActuatorStartUpAmountsQuery();
    }
}