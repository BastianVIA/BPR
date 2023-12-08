using Frontend.Entities;
using Frontend.Networking;

namespace Frontend.Model;

public class ActuatorComponentHistoryModel : IActuatorComponentHistoryModel
{
    private readonly INetwork _network;

    public ActuatorComponentHistoryModel(INetwork network)
    {
        _network = network;
    }

    public async Task<List<ComponentChange>> GetComponentHistory(int woNo, int serialNo)
    {
        var networkResponse = await _network.GetComponentHistory(woNo, serialNo);
        return networkResponse.Changes.Select(change => new ComponentChange()
        {
            OldPCBAUid = change.OldPCBAUid, 
            RemovalTime = change.RemovalTime
        }).ToList();
    }
}