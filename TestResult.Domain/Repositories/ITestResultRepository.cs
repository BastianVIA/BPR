using TestResult.Domain.Entities;

namespace TestResult.Domain.Repositories;

public interface ITestResultRepository
{
    Task CreateTestResult(Entities.TestResult testResult);
    Task<Entities.TestResult> GetTestResult(int woNo, int serialNo);
    Task<Entities.TestResult> GetActuatorTestDetails(CompositeActuatorId id);
}