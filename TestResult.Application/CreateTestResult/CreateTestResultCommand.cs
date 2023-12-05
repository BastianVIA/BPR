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

    private CreateTestResultCommand()
    {
    }

    private CreateTestResultCommand(int workOrderNumber, int serialNumber, string tester, int bay, 
        string minServoPosition, string maxServoPosition, string minBuslinkPosition, string maxBuslinkPosition)
    {
        WorkOrderNumber = workOrderNumber;
        SerialNumber = serialNumber;
        Tester = tester;
        Bay = bay;
        MinServoPosition = minServoPosition;
        MaxServoPosition = maxServoPosition;
        MinBuslinkPosition = minBuslinkPosition;
        MaxBuslinkPosition = maxBuslinkPosition;
    }

    public static CreateTestResultCommand Create(int workOrderNumber, int serialNumber, string tester, int bay, 
        string minServoPosition, string maxServoPosition, string minBuslinkPosition, string maxBuslinkPosition)
    {
        return new CreateTestResultCommand(workOrderNumber, serialNumber, tester, bay, minServoPosition, 
            maxServoPosition, minBuslinkPosition, maxBuslinkPosition);
    }
}