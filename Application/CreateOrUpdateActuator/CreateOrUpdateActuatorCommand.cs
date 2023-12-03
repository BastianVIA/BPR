using BuildingBlocks.Application;

namespace Application.CreateOrUpdateActuator;

public class CreateOrUpdateActuatorCommand : ICommand
{
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
    public string PCBAUid { get; private set; }

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