using BuildingBlocks.Integration;

namespace LINTest.Integration;

public class ActuatorTestInvalidIntegrationTest : IntegrationEvent
{
    public int WorkOrderNumber { get; }
    public int SerailNumber { get; }
    public string PCBAUid { get; }
    
    public string CommunicationProtocol { get; }
    public string ArticleNumber { get; }
    public string ArticleName { get; }
    public DateTime CreatedTime { get; }

    public ActuatorTestInvalidIntegrationTest(int workOrderNumber, int serailNumber, string pcbaUid,string articleNo,
        string articleName, string communicationProtocol, DateTime createdTime)
    {
        WorkOrderNumber = workOrderNumber;
        SerailNumber = serailNumber;
        PCBAUid = pcbaUid;
        ArticleNumber = articleNo;
        ArticleName = articleName;
        CommunicationProtocol = communicationProtocol;
        CreatedTime = createdTime;
    }
    
}