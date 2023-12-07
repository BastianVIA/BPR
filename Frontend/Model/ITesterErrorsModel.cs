using Frontend.Entities;
using Frontend.Service;

namespace Frontend.Model;

public interface ITesterErrorsModel
{
    Task<List<TesterErrorsSet>> GetTestErrorsForTesters(List<string> testers, TesterTimePeriodEnum timePeriod);
}