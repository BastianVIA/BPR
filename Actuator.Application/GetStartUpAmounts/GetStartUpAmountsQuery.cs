using BuildingBlocks.Application;

namespace Application.GetStartUpAmounts;

public class GetStartUpAmountsQuery : IQuery<GetStartUpAmountsDto>
{
    private GetStartUpAmountsQuery()
    {
    }

    public static GetStartUpAmountsQuery Create()
    {
        return new GetStartUpAmountsQuery();
    }
}