using Frontend.Entities;
using Frontend.Networking;
using Frontend.Service;

namespace Frontend.Model;

public class ErrorStatisticsModel : IErrorStatisticsModel
{
    private INetwork _network;

    public ErrorStatisticsModel(INetwork network)
    {
        _network = network;
    }

    public async Task<List<TesterErrorsSet>> GetTestErrorsForTesters(List<string> testers, TesterTimePeriodEnum timePeriod)
    {
        var response = await _network.GetTestErrorForTesters(testers, timePeriod);
        return response.ErrorsForTesters.Select(tester => new TesterErrorsSet
        {
            Name = tester.Name, Errors = FromResponse(tester.Errors)
        }).ToList();
    }

    public async Task<List<string>> GetAllCellNames()
    {
        var response = await _network.GetAllCellNames();
        return response.AllTesters.ToList();
    }

    private List<TesterErrorEntry> FromResponse(IEnumerable<GetTestErrorForTestersError> errors)
    {
        return errors.Select(error => new TesterErrorEntry{ DateString = error.Date.ToString(), DateDouble = error.Date.ToOADate(), ErrorCount = error.ErrorCount }).ToList();
    }
}