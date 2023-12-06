using BuildingBlocks.Application;

namespace TestResult.Application.GetTestResultsWithFilter;

public class GetTestResultsWithFilterQuery : IQuery<GetTestResultsWithFilterDto>
{
    public int? WorkOrderNumber { get; set; }
    public int? SerialNumber { get; set; }
    public string? Tester { get; set; }
    public int? Bay { get; set; }

    public GetTestResultsWithFilterQuery()
    {
    }
    
    public void Validate()
    {
        if (WorkOrderNumber == null && SerialNumber == null && Tester == null && Bay == null)
        {
            throw new ArgumentException("Must specify at least one search parameter");
        }
        
    }
    

}