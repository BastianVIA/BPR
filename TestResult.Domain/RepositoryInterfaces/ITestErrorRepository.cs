using TestResult.Domain.Entities;

namespace TestResult.Domain.RepositoryInterfaces;

public interface ITestErrorRepository
{
    Task CreateTestError(TestError testError);
    Task<List<TestError>> GetTestErrorsWithFilter(int? woNo, string? tester,
        int? bay, int? errorCode,
        DateTime? startDate, DateTime? endDate);
    Task<List<TestError>> GetAllErrorsForTesterSince(List<string> testers, DateTime startDate, DateTime endDate);
    Task<int> GetTotalTestErrorAmounts();
}