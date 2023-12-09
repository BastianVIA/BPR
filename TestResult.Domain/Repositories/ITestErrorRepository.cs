using TestResult.Domain.Entities;

namespace TestResult.Domain.Repositories;

public interface ITestErrorRepository
{
    public Task CreateTestError(TestError testError);
    public Task<List<TestError>> GetTestErrorsWithFilter(int? woNo, string? tester,
        int? bay, int? errorCode,
        DateTime? startDate, DateTime? endDate);
    public Task<List<TestError>> GetAllErrorsForTesterSince(List<string> testers, DateTime startDate, DateTime endDate);
    public Task<int> GetTotalTestErrorAmounts();
}