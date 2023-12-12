using Frontend.Entities;
using Frontend.Service;

namespace Frontend.Model;

public interface IErrorStatisticsModel
{
    Task<List<TesterErrorsSet>> GetTestErrorsForTesters(List<string> testers, TesterTimePeriodEnum timePeriod);
    Task<List<string>> GetAllCellNames();
}