using BuildingBlocks.Application;

namespace TestResult.Application.GetActuatorTestDetails;

public class GetActuatorTestDetailsQuery : IQuery<GetActuatorTestDetailsDto>
{
    public int? WorkOrderNumber { get; set; }
    public int? SerialNumber { get; set; }
    public string? Tester { get; set; }
    public int? Bay { get; set; }

    public GetActuatorTestDetailsQuery()
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