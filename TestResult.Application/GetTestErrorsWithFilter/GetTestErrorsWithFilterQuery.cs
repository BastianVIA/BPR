using BuildingBlocks.Application;

namespace TestResult.Application.GetTestErrorsWithFilter;

public class GetTestErrorsWithFilterQuery : IQuery<GetTestErrorsWithFilterDto>
{
    public int TimeIntervalBetweenRowsAsMinutes { get; set; }
    public int? WorkOrderNumber { get; set; }
    public string? Tester { get; set; }
    public int? Bay { get; set; }
    public int? ErrorCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public GetTestErrorsWithFilterQuery()
    {}
    public void Validate()
    {
        if (WorkOrderNumber == null && ErrorCode == null && Tester == null && Bay == null)
        {
            throw new ArgumentException("Must specify at least one search parameter");
        }
    }
}