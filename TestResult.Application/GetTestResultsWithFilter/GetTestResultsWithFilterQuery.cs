using BuildingBlocks.Application;

namespace TestResult.Application.GetTestResultsWithFilter;

public class GetTestResultsWithFilterQuery : IQuery<GetTestResultsWithFilterDto>
{
    public int? WorkOrderNumber { get; set; }
    public int? SerialNumber { get; set; }
    public string? Tester { get; set; }
    public int? Bay { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public GetTestResultsWithFilterQuery()
    {
    }
    
    public void Validate()
    {
        if (WorkOrderNumber is null && SerialNumber is null && Tester is null && Bay is null 
            && StartDate is null && EndDate is null)
        {
            throw new ArgumentException("Must specify at least one search parameter");
        }
    }
}