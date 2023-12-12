using BuildingBlocks.Application;

namespace TestResult.Application.CreateTestResult;

public class CreateTestResultCommand : ICommand
{
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

    private CreateTestResultCommand()
    {
    }

    private CreateTestResultCommand(int woNo, int serialNo, string tester, int bay, 
        string minServoPosition, string maxServoPosition, string minBuslinkPosition, string maxBuslinkPosition, 
        string servoStroke, DateTime timeOccured)
    {
        WorkOrderNumber = woNo;
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

    public static CreateTestResultCommand Create(int woNo, int serialNo, string tester, int bay, 
        string minServoPosition, string maxServoPosition, string minBuslinkPosition, string maxBuslinkPosition, 
        string servoStroke, DateTime timeOccured)
    {
        return new CreateTestResultCommand(woNo, serialNo, tester, bay, minServoPosition, 
            maxServoPosition, minBuslinkPosition, maxBuslinkPosition, servoStroke, timeOccured);
    }
}