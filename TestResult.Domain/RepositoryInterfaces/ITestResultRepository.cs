namespace TestResult.Domain.RepositoryInterfaces;

public interface ITestResultRepository
{
    Task CreateTestResult(Entities.TestResult testResult);
    Task<List<Domain.Entities.TestResult>> GetActuatorsTestDetails(int? woNo, int? serialNo, string? tester, int? bay,DateTime? startDate, DateTime? endDate);
    Task<int> GetNumberOfTestsPerformedInInterval(DateTime startTime, DateTime endTime);
    Task<List<string>> GetAllTesters();
    Task<int> GetTotalTestResultAmount();
    Task<int> GetTotalTestResultWithErrorsAmount();
    Task<int> GetTotalTestResultWithoutErrorsAmount();
}