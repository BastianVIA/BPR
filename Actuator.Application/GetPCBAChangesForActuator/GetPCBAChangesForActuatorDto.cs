using Domain.Entities;

namespace Application.GetPCBAChangesForActuator;

public class GetPCBAChangesForActuatorDto
{
    public List<GetPCBAChangesForActuatorChangeDto> Changes { get; }

    private GetPCBAChangesForActuatorDto()
    {
    }

    public GetPCBAChangesForActuatorDto(List<GetPCBAChangesForActuatorChangeDto> changes)
    {
        Changes = changes;
    }

    internal static GetPCBAChangesForActuatorDto From(List<ActuatorPCBAChange> pcbaChanges)
    {
        List<GetPCBAChangesForActuatorChangeDto> changes = new();
        foreach (var change in pcbaChanges)
        {
            changes.Add(GetPCBAChangesForActuatorChangeDto.From(change));
        }

        return new GetPCBAChangesForActuatorDto(changes);
    }
}

public class GetPCBAChangesForActuatorChangeDto
{
    public int WorkOrderNumber { get; }
    public int SerialNumber { get; }
    public string OldPCBAUid { get; }
    public DateTime RemovalTime { get; }

    private GetPCBAChangesForActuatorChangeDto()
    {
    }

    private GetPCBAChangesForActuatorChangeDto(int woNo, int serialNo, string oldPCBAUid, DateTime removalTime)
    {
        OldPCBAUid = oldPCBAUid;
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        RemovalTime = removalTime;
    }

    internal static GetPCBAChangesForActuatorChangeDto From(ActuatorPCBAChange change)
    {
        return new GetPCBAChangesForActuatorChangeDto(change.WorkOrderNumber, change.SerialNumber, change.OldPCBAUid,
            change.RemovalTime);
    }
}