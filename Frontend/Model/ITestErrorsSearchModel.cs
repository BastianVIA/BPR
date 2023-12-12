using Frontend.Entities;
using Frontend.Util;

namespace Frontend.Model;

public interface ITestErrorsSearchModel
{
    Task<TestErrorResponse> GetTestErrorsWithFilter(int? woNo, string? tester,
        int? bay, int? errorCode, DateTime startDate, DateTime? endDate, int timeIntervalBetweenRowsAsMinutes);
}