using TestResult.Domain.Entities;

namespace TestResult.Domain.Repositories;

public interface ITestErrorRepository
{
    Task CreateTestError(TestError testError);
}