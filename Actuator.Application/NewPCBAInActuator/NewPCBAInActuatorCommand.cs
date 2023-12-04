using BuildingBlocks.Application;

namespace Application.NewPCBAInActuator;

public class NewPCBAInActuatorCommand : ICommand
{
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
    public string PCBAUid { get; private set; }
    
    private NewPCBAInActuatorCommand()
    {
    }
    
    private NewPCBAInActuatorCommand(int woNo, int serialNo, string pcbaUid)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        PCBAUid = pcbaUid;
    }

    public static NewPCBAInActuatorCommand Create(int woNo, int serialNo, string pcbaUid)
    {
        return new NewPCBAInActuatorCommand(woNo, serialNo, pcbaUid);
    }
}