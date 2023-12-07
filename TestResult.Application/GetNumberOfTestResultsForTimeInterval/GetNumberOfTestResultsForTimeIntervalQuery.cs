using BuildingBlocks.Application;

namespace TestResult.Application.GetNumberOfTestResultsForTimeInterval;

public class GetNumberOfTestResultsForTimeIntervalQuery : IQuery<int>
{
    internal DateTime StartTime { get; }
    internal DateTime EndTime { get; }
    
    private GetNumberOfTestResultsForTimeIntervalQuery(){}
    private GetNumberOfTestResultsForTimeIntervalQuery(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    public static GetNumberOfTestResultsForTimeIntervalQuery From(DateTime startTime, DateTime endTime)
    {
        return new GetNumberOfTestResultsForTimeIntervalQuery(startTime, endTime);
    }

}