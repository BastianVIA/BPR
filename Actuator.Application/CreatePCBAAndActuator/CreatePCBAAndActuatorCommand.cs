using BuildingBlocks.Application;

namespace Application.CreatePCBAAndActuator;

public class CreatePCBAAndActuatorCommand : ICommand
{
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
    public string PCBAUid { get; private set; }

    private CreatePCBAAndActuatorCommand()
    {
    }
    
    private CreatePCBAAndActuatorCommand(int woNo, int serialNo, string pcbaUid)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        PCBAUid = pcbaUid;
    }

    public static CreatePCBAAndActuatorCommand Create(int woNo, int serialNo, string pcbaUid)
    {
        return new CreatePCBAAndActuatorCommand(woNo, serialNo, pcbaUid);
    }
}