using BuildingBlocks.Integration;

namespace LINTest.Integration;

public class ActuatorTestSucceededIntegrationEvent : IntegrationEvent
{
    public int WorkOrderNumber { get; }
    public int SerialNumber { get; }
    public string PCBAUid { get; }
    public string CommunicationProtocol { get; }
    public string ArticleNumber { get; }
    public string ArticleName { get; }
    public DateTime CreatedTime { get; }

    public ActuatorTestSucceededIntegrationEvent(int workOrderNumber, int serialNumber, string pcbaUid, string articleNo,
        string articleName, string communicationProtocol, DateTime createdTime)
    {
        WorkOrderNumber = workOrderNumber;
        SerialNumber = serialNumber;
        PCBAUid = pcbaUid;
        ArticleNumber = articleNo;
        ArticleName = articleName;
        CommunicationProtocol = communicationProtocol;
        CreatedTime = createdTime;
    }
}