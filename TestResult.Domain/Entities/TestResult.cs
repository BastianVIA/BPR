using BuildingBlocks.Domain;

namespace TestResult.Domain.Entities;

public class TestResult : Entity
{
    public Guid Id { get; set; }
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
    public string Tester { get; private set; }
    public int Bay { get; private set; }
    public string MinServoPosition { get; private set; }
    public string MaxServoPosition { get; private set; }
    public string MinBuslinkPosition { get; private set; }
    public string MaxBuslinkPosition { get; private set; }
    public string ServoStroke { get; private set; }
    public DateTime TimeOccured { get; private set; }

    public TestResult(Guid id, int workOrderNo, int serialNo, string tester, int bay, string minServoPosition, 
        string maxServoPosition, string minBuslinkPosition, string maxBuslinkPosition, string servoStroke, 
        DateTime timeOccured)
    {
        Id = id;
        WorkOrderNumber = workOrderNo;
        SerialNumber = serialNo;
        Tester = tester;
        Bay = bay;
        MinServoPosition = minServoPosition;
        MaxServoPosition = maxServoPosition;
        MinBuslinkPosition = minBuslinkPosition;
        MaxBuslinkPosition = maxBuslinkPosition;
        ServoStroke = servoStroke;
        TimeOccured = timeOccured;
    }

    public static TestResult Create(int workOrderNo, int serialNo, string tester, int bay, string minServoPosition, 
        string maxServoPosition, string minBuslinkPosition, string maxBuslinkPosition, string servoStroke, 
        DateTime timeOccured)
    {
        var id = Guid.NewGuid();
        return new TestResult(id, workOrderNo, serialNo, tester, bay, minServoPosition, maxServoPosition, 
            minBuslinkPosition, maxBuslinkPosition, servoStroke, timeOccured);
    }
}