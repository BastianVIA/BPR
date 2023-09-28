using BuildingBlocks.Application;

namespace Application.GetActuatorDetails;

public class GetActuatorDetailsQuery : IQuery<GetActuatorDetailsDto>
{
    internal int WorkOrderNumber { get; }
    internal int SerialNumber { get; }

    private GetActuatorDetailsQuery()
    {
        
    }
    private GetActuatorDetailsQuery(int woNo, int serialNo)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
    }

    public static GetActuatorDetailsQuery Create(int woNo, int serialNo)
    {
        return new GetActuatorDetailsQuery(woNo, serialNo); 
    }
}