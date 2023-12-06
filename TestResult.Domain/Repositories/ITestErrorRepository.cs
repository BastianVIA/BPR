using TestResult.Domain.Entities;

namespace TestResult.Domain.Repositories;

public interface ITestErrorRepository
{
    Task CreateTestError(TestError testError);

    Task<List<TestError>> GetTestErrorsWithFilter(int? woNo, string? tester,
        int? bay, int? errorCode,
        DateTime? startDate, DateTime? endDate);
}