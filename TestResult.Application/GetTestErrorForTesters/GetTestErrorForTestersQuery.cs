using BuildingBlocks.Application;

namespace TestResult.Application.GetTestErrorForTesters;

public class GetTestErrorForTestersQuery : IQuery<GetTestErrorForTestersDto>
{
    internal List<string> Testers { get; }
    internal TesterTimePeriodEnum TimePeriod { get; }
 
    private GetTestErrorForTestersQuery(){}
    private GetTestErrorForTestersQuery(List<string> testers, TesterTimePeriodEnum timePeriod)
    {
        Testers = testers;
        TimePeriod = timePeriod;
    }   
    public static GetTestErrorForTestersQuery Create(List<string> testers, TesterTimePeriodEnum timePeriod)
    {
        return new GetTestErrorForTestersQuery(testers, timePeriod);
    }
}