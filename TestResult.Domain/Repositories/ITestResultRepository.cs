namespace TestResult.Domain.Repositories;

public interface ITestResultRepository
{
    Task CreateTestResult(Entities.TestResult testResult);
    Task<Entities.TestResult> GetTestResult(int woNo, int serialNo);
}