using Frontend.Networking;

namespace Frontend.Model;

public class UpdateActuatorsPCBAModel : IUpdateActuatorsPCBAModel
{
    private readonly INetwork _network;

    public UpdateActuatorsPCBAModel(INetwork network)
    {
        _network = network;
    }

    public async Task UpdateActuatorsPCBA(int woNo, int serialNo, string pcbaUid)
    {
        await _network.UpdateActuatorsPCBA(woNo, serialNo, pcbaUid);
    }
}