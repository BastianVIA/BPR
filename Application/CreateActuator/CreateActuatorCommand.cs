using BuildingBlocks.Application;
using Domain.Entities;

namespace Application.CreateActuator;

public class CreateActuatorCommand : ICommand
{
    internal int WorkOrderNumber { get; private set; }
    internal int SerialNumber { get; private set; }
    internal string PCBAUid { get; private set; }

    private CreateActuatorCommand()
    {
    }

    private CreateActuatorCommand(int woNo, int serialNo, string pcbaUid)
    {
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        PCBAUid = pcbaUid;
    }

    public static CreateActuatorCommand Create(int woNo, int serialNo, string pcbaUid)
    {
        return new CreateActuatorCommand(woNo, serialNo, pcbaUid);
    }
}