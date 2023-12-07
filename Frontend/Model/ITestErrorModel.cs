using Frontend.Entities;

namespace Frontend.Model;

public interface ITestErrorModel
{
    Task<TestError> GetTestErrorsWithFilter(int? wrkOrderNumber, string? tester,
        int? bay, int? errorCode, DateTime startDate, DateTime endDate, int timeIntervalBetweenRowsAsMinutes);
}