using BuildingBlocks.Application;

namespace Application.GetPCBAChangesForActuator;

public class GetPCBAChangesForActuatorQuery : IQuery<GetPCBAChangesForActuatorDto>
{
    internal int WorkOrderNumber { get; }
    internal int SerialNumber { get; }

    private GetPCBAChangesForActuatorQuery()
    {
    }

    private GetPCBAChangesForActuatorQuery(int woNo, int serialNo)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
    }

    public static GetPCBAChangesForActuatorQuery Create(int woNo, int serialNo)
    {
        return new GetPCBAChangesForActuatorQuery(woNo, serialNo);
    }
}