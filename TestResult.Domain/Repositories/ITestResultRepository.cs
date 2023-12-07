using TestResult.Domain.Entities;

namespace TestResult.Domain.Repositories;

public interface ITestResultRepository
{
    Task CreateTestResult(Entities.TestResult testResult);
    Task<List<Domain.Entities.TestResult>> GetActuatorsTestDetails(int? woNo, int? serialNo, string? tester, int? bay);
    Task<int> GetNumberOfTestsPerformedInInterval(DateTime startTime, DateTime endTime);
    Task<List<string>> GetAllTesters();
}