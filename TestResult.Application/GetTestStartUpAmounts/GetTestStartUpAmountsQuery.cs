using BuildingBlocks.Application;

namespace TestResult.Application.GetStartUpAmounts;

public class GetTestStartUpAmountsQuery : IQuery<GetTestStartUpAmountsDto>
{
    private GetTestStartUpAmountsQuery()
    {
    }

    public static GetTestStartUpAmountsQuery Create()
    {
        return new GetTestStartUpAmountsQuery();
    }
}