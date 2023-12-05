using BuildingBlocks.Application;

namespace TestResult.Application.GetActuatorTestDetails;

public class GetActuatorTestDetailsQuery : IQuery<GetActuatorTestDetailsDto>
{
    internal int WorkOrderNumber { get; }
    internal int SerialNumber { get; }

    private GetActuatorTestDetailsQuery()
    {
    }

    public GetActuatorTestDetailsQuery(int woNo, int serialNo)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
    }

    public static GetActuatorTestDetailsQuery Create(int woNo, int serialNo)
    {
        return new GetActuatorTestDetailsQuery(woNo, serialNo);
    }
}