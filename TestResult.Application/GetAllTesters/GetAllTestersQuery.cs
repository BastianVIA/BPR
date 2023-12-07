using BuildingBlocks.Application;

namespace TestResult.Application.GetAllTesters;

public class GetAllTestersQuery : IQuery<GetAllTestersDto>
{
    
    private GetAllTestersQuery(){}

    public static GetAllTestersQuery Create()
    {
        return new GetAllTestersQuery();
    }
}