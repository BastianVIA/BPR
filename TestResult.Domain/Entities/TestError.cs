using BuildingBlocks.Domain;
using TestResult.Domain.Events;

namespace TestResult.Domain.Entities;

public class TestError : Entity
{
    public Guid Id { get; set; }
    public int? WorkOrderNumber { get; private set; }
    public int? SerialNumber { get; private set; }
    public string Tester { get; private set; }
    public int Bay { get; private set; }
    public int ErrorCode { get; private set; }
    public string ErrorMessage { get; private set; }
    public DateTime TimeOccured { get; private set; }

    public TestError(Guid id,  string tester, int bay, int errorCode, 
        string errorMessage, DateTime timeOccured, int? woNo = null, int? serialNo = null)
    {
        Id = id;
        WorkOrderNumber = woNo;
        SerialNumber = serialNo;
        Tester = tester;
        Bay = bay;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        TimeOccured = timeOccured;
    }

    public static TestError Create(int woNo, int serialNo, string tester, int bay, int errorCode, 
        string errorMessage, DateTime timeOccured)
    {
        var id = Guid.NewGuid();
        var testError = new TestError(id,  tester, bay, errorCode, errorMessage, timeOccured, woNo, serialNo);

        testError.AddDomainEvent(new TestErrorCreatedDomainEvent(testError.Id));
        
        return testError;
    }
}