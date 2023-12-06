using BuildingBlocks.Integration;

namespace LINTest.Integration;

public class ActuatorTestFailedIntegrationEvent : IntegrationEvent
{
    public int WorkOrderNumber { get; }
    public int SerialNumber { get; }
    public string Tester { get; }
    public int Bay { get; }
    public int ErrorCode { get; }
    public string ErrorMessage { get; }
    public DateTime TimeOccured { get; }

    public ActuatorTestFailedIntegrationEvent(int workOrderNumber, int serialNumber, string tester, int bay, 
        int errorCode, string errorMessage, DateTime timeOccured)
    {
        WorkOrderNumber = workOrderNumber;
        SerialNumber = serialNumber;
        Tester = tester;
        Bay = bay;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        TimeOccured = timeOccured;
    }
}