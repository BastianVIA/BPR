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
    public string Tester { get; }
    public int Bay { get; }
    public string MinServoPosition { get; }
    public string MaxServoPosition { get; }
    public string MinBuslinkPosition { get; }
    public string MaxBuslinkPosition { get; }
    public string ServoStroke { get; }

    public ActuatorTestSucceededIntegrationEvent(int workOrderNumber, int serialNumber, string pcbaUid, string articleNo,
        string articleName, string communicationProtocol, DateTime createdTime, string tester, int bay, 
        string minServoPosition, string maxServoPosition, string minBuslinkPosition, 
        string maxBuslinkPosition, string servoStroke)
    {
        WorkOrderNumber = workOrderNumber;
        SerialNumber = serialNumber;
        PCBAUid = pcbaUid;
        ArticleNumber = articleNo;
        ArticleName = articleName;
        CommunicationProtocol = communicationProtocol;
        CreatedTime = createdTime;
        Tester = tester;
        Bay = bay;
        MinServoPosition = minServoPosition;
        MaxServoPosition = maxServoPosition;
        MinBuslinkPosition = minBuslinkPosition;
        MaxBuslinkPosition = maxBuslinkPosition;
        ServoStroke = servoStroke;
    }
}