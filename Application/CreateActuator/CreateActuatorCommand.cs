using BuildingBlocks.Application;

namespace Application.CreateActuator;

public class CreateActuatorCommand:ICommand
{
   
    internal int WorkOrderNumber { get; }
    internal int SerialNumber { get; }
    internal int PCBAUid { get; }

    private CreateActuatorCommand()
    {
    }
    
    private CreateActuatorCommand(int woNo, int serialNo, int pcbaUid)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        PCBAUid = pcbaUid;
    }

    public static CreateActuatorCommand Create(int woNo, int serialNo, int pcbaUid)
    {
        return new CreateActuatorCommand(woNo, serialNo, pcbaUid);
    }
}