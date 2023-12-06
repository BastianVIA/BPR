using BuildingBlocks.Domain;
using TestResult.Domain.Events;

namespace TestResult.Domain.Entities;

public class TestResult : Entity
{
    public Guid Id { get; set; }
    public int WorkOrderNumber { get; set; }
    public int SerialNumber { get; set; }
    public string Tester { get; set; }
    public int Bay { get; private set; }
    public string MinServoPosition { get; private set; }
    public string MaxServoPosition { get; private set; }
    public string MinBuslinkPosition { get; private set; }
    public string MaxBuslinkPosition { get; private set; }
    public string ServoStroke { get; private set; }
    public DateTime TimeOccured { get; private set; }


    public TestResult(Guid id, int woNo, int serialNo, string tester, int bay, string minServoPosition, 
        string maxServoPosition, string minBuslinkPosition, string maxBuslinkPosition, string servoStroke, DateTime timeOccured)
    {
        Id = id;
        Tester = tester;
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        Bay = bay;
        MinServoPosition = minServoPosition ;
        MaxServoPosition = maxServoPosition;
        MinBuslinkPosition = minBuslinkPosition;
        MaxBuslinkPosition = maxBuslinkPosition;
        ServoStroke = servoStroke;
        TimeOccured = timeOccured;
    }

    public static TestResult Create(int workOrderNo, int serialNo, string tester, int bay, string? minServoPosition, 
        string? maxServoPosition, string? minBuslinkPosition, string? maxBuslinkPosition, string? servoStroke, 
        DateTime timeOccured)
    {
        minServoPosition??= "N/A";
        maxServoPosition??= "N/A";
        minBuslinkPosition??= "N/A";
        maxBuslinkPosition??= "N/A";
        servoStroke??= "N/A";
        
        var id = Guid.NewGuid();
        var testResult = new TestResult(id, workOrderNo, serialNo, tester, bay, minServoPosition, maxServoPosition, 
            minBuslinkPosition, maxBuslinkPosition, servoStroke, timeOccured);
        
        testResult.AddDomainEvent(new TestResultCreatedDomainEvent(testResult.Id));
        
        return testResult;
    }
}