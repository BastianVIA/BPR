using Frontend.Entities;
using Frontend.Util;

namespace Frontend.Model;

public interface ITestErrorModel
{
    Task<TestErrorResponse> GetTestErrorsWithFilter(int? wrkOrderNumber, string? tester,
        int? bay, int? errorCode, DateTime startDate, DateTime? endDate, int timeIntervalBetweenRowsAsMinutes);
}