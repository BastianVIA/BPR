using BuildingBlocks.Integration;

namespace LINTest.Integration;

public class ActuatorTestFailedIntegrationEvent : IntegrationEvent
{
    public int WorkOrderNumber { get; }
    public int SerailNumber { get; }
    public string PCBAUid { get; }

    public ActuatorTestFailedIntegrationEvent(int workOrderNumber, int serailNumber, string pcbaUid)
    {
        WorkOrderNumber = workOrderNumber;
        SerailNumber = serailNumber;
        PCBAUid = pcbaUid;
    }
}