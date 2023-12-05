using Frontend.Entities;

namespace Frontend.Model;

public interface ITestResultSearchModel
{
    public Task<List<TestResult>> GetTestResultsWithFilter(int? woNo);
}