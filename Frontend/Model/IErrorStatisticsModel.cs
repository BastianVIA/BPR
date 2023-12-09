using Frontend.Entities;
using Frontend.Service;

namespace Frontend.Model;

public interface IErrorStatisticsModel
{
    public Task<List<TesterErrorsSet>> GetTestErrorsForTesters(List<string> testers, TesterTimePeriodEnum timePeriod);
    public Task<List<string>> GetAllCellNames();
}