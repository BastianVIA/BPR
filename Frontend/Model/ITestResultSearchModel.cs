using Frontend.Entities;

namespace Frontend.Model;

public interface ITestResultSearchModel
{
    Task<List<TestResult>> GetTestResultsWithFilter(int? woNo, int? serialNo, string? tester, int? bay, DateTime? startDate, DateTime? endDate);
}