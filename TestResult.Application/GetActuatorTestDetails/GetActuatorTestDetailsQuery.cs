using BuildingBlocks.Application;

namespace TestResult.Application.GetActuatorTestDetails;

public class GetActuatorTestDetailsQuery : IQuery<GetActuatorTestDetailsDto>
{
    internal int? WorkOrderNumber { get; }
    internal int? SerialNumber { get; }
    public string? Tester { get; }
    public int? Bay { get; }

    private GetActuatorTestDetailsQuery(int? woNo, int? serialNo, string? tester, int? bay)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        Tester = tester;
        Bay = bay;
    }

    public static GetActuatorTestDetailsQuery Create(int? woNo, int? serialNo, string? tester, int? bay)
    {
        if (woNo == null && serialNo == null && tester == null && bay == null)
        {
            throw new ArgumentException("Must specify at least one search parameter");
        }

        return new GetActuatorTestDetailsQuery(woNo, serialNo, tester, bay);
    }
}