using BuildingBlocks.Application;

namespace Application.CreateOrUpdateActuator;

public class CreateOrUpdateActuatorCommand : ICommand
{
    internal int WorkOrderNumber { get; }
    internal int SerialNumber { get; }
    internal int PCBAUid { get; }

    private CreateOrUpdateActuatorCommand()
    {
    }
    
    private CreateOrUpdateActuatorCommand(int woNo, int serialNo, int pcbaUid)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        PCBAUid = pcbaUid;
    }

    public static CreateOrUpdateActuatorCommand Create(int woNo, int serialNo, int pcbaUid)
    {
        return new CreateOrUpdateActuatorCommand(woNo, serialNo, pcbaUid);
    }
}