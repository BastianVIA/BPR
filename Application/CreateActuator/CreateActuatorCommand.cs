using BuildingBlocks.Application;
using Domain.Entities;

namespace Application.CreateActuator;

public class CreateActuatorCommand:ICommand
{
   
    internal int WorkOrderNumber { get; }
    internal int SerialNumber { get; }
    internal PCBA PCBA { get; }

    private CreateActuatorCommand()
    {
    }
    
    private CreateActuatorCommand(int woNo, int serialNo, string pcbaUid)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        PCBA = new PCBA(pcbaUid, 0);
    }

    public static CreateActuatorCommand Create(int woNo, int serialNo, string pcbaUid)
    {
        return new CreateActuatorCommand(woNo, serialNo, pcbaUid);
    }
}