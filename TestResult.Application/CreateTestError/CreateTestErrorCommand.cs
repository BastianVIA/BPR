using BuildingBlocks.Application;

namespace TestResult.Application.CreateTestError;

public class CreateTestErrorCommand : ICommand
{
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
    public string Tester { get; private set; }
    public int Bay { get; private set; }
    public int ErrorCode { get; private set; }
    public string ErrorMessage { get; private set; }
    public DateTime TimeOccured { get; private set; }

    private CreateTestErrorCommand()
    {
    }

    private CreateTestErrorCommand(int workOrderNumber, int serialNumber, string tester, int bay, int errorCode, 
        string errorMessage, DateTime timeOccured)
    {
        WorkOrderNumber = workOrderNumber;
        SerialNumber = serialNumber;
        Tester = tester;
        Bay = bay;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        TimeOccured = timeOccured;
    }

    public static CreateTestErrorCommand Create(int workOrderNumber, int serialNumber, string tester, int bay,
        int errorCode, string errorMessage, DateTime timeOccured)
    {
        return new CreateTestErrorCommand(workOrderNumber, serialNumber, tester, bay, errorCode, errorMessage, timeOccured);
    }
}