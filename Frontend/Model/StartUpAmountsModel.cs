using Frontend.Networking;
using Frontend.Util;

namespace Frontend.Model;

public class StartUpAmountsModel : IStartUpAmountsModel
{
    private INetwork _network;

    public StartUpAmountsModel(INetwork network)
    {
        _network = network;
    }

    public async Task<StartUpAmounts> GetStartUpAmounts()
    {
        var networkResponse = await _network.GetStartUpAmounts();
        return new StartUpAmounts()
        {
            ActuatorAmount = networkResponse.ActuatorAmount,
            TestResultAmount = networkResponse.TestResultAmount,
            TestErrorAmount = networkResponse.TestErrorAmount,
            TestResultWithErrorAmount = networkResponse.TestResultWithErrorAmount,
            TestResultWithoutErrorAmount = networkResponse.TestResultWithoutErrorAmount
        };
    }
}