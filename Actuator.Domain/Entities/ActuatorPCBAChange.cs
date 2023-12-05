using BuildingBlocks.Domain;

namespace Domain.Entities;

public class ActuatorPCBAChange : Entity
{
    public int WorkOrderNumber { get; }
    public int SerialNumber { get; }
    public string OldPCBAUid { get; }
    public DateTime RemovalTime { get; }

    private ActuatorPCBAChange() { }
    
    public ActuatorPCBAChange(int woNo, int serialNo, string oldPCBAUid, DateTime removalTime)
    {
        RemovalTime = removalTime;
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        OldPCBAUid = oldPCBAUid;
    }

    public static ActuatorPCBAChange Create(int woNo, int serialNo, string oldPCBAUid, DateTime removalTime)
    {
        return new ActuatorPCBAChange(woNo, serialNo, oldPCBAUid, removalTime);
    }
}