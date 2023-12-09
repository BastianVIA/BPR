namespace TestResult.Domain.Repositories;

public interface ITestResultRepository
{
    public Task CreateTestResult(Entities.TestResult testResult);
    public Task<List<Domain.Entities.TestResult>> GetActuatorsTestDetails(int? woNo, int? serialNo, string? tester, int? bay,DateTime? startDate, DateTime? endDate);
    public Task<int> GetNumberOfTestsPerformedInInterval(DateTime startTime, DateTime endTime);
    public Task<List<string>> GetAllTesters();
    public Task<int> GetTotalTestResultAmount();
    public Task<int> GetTotalTestResultWithErrorsAmount();
    public Task<int> GetTotalTestResultWithoutErrorsAmount();
}