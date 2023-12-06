using BuildingBlocks.Domain;

namespace TestResult.Domain.Entities;

public class TestError : Entity
{
    public Guid Id { get; set; }
    public int WorkOrderNumber { get; private set; }
    public int SerialNumber { get; private set; }
    public string Tester { get; private set; }
    public int Bay { get; private set; }
    public int ErrorCode { get; private set; }
    public string ErrorMessage { get; private set; }
    public DateTime TimeOccured { get; private set; }

    public TestError(Guid id, int workOrderNumber, int serialNumber, string tester, int bay, int errorCode, 
        string errorMessage, DateTime timeOccured)
    {
        Id = id;
        WorkOrderNumber = workOrderNumber;
        SerialNumber = serialNumber;
        Tester = tester;
        Bay = bay;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        TimeOccured = timeOccured;
    }

    public static TestError Create(int workOrderNumber, int serialNumber, string tester, int bay, int errorCode, 
        string errorMessage, DateTime timeOccured)
    {
        var id = Guid.NewGuid();
        return new TestError(id, workOrderNumber, serialNumber, tester, bay, errorCode, errorMessage, timeOccured);
    }
}