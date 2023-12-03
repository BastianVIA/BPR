using BuildingBlocks.Integration;

namespace LINTest.Integration;

public class ActuatorTestSucceededIntegrationEvent : IntegrationEvent
{
    public int WorkOrderNumber { get; }
    public int SerailNumber { get; }
    public string PCBAUid { get; }

    public ActuatorTestSucceededIntegrationEvent(int workOrderNumber, int serailNumber, string pcbaUid)
    {
        WorkOrderNumber = workOrderNumber;
        SerailNumber = serailNumber;
        PCBAUid = pcbaUid;
    }
}