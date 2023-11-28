using BuildingBlocks.Application;

namespace Application.CreateOrUpdateActuator;

public class CreateOrUpdateActuatorCommand : ICommand
{
    internal int WorkOrderNumber { get; }
    internal int SerialNumber { get; }
    internal string PCBAUid { get; }

    private CreateOrUpdateActuatorCommand()
    {
    }
    
    private CreateOrUpdateActuatorCommand(int woNo, int serialNo, string pcbaUid)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        PCBAUid = pcbaUid;
    }

    public static CreateOrUpdateActuatorCommand Create(int woNo, int serialNo, string pcbaUid)
    {
        return new CreateOrUpdateActuatorCommand(woNo, serialNo, pcbaUid);
    }
}