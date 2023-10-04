using Frontend.Core;
using Frontend.Entities;
using Frontend.Service;

namespace Frontend.Model;

public class ActuatorDetailsModel : IActuatorDetailsModel
{
    private INetworkAdapter _network;

    public ActuatorDetailsModel(INetworkAdapter network)
    {
        _network = network;
    }
    
    public async Task<Actuator> GetActuatorDetails(int woNo, int serialNo)
    {
        var networkResponse = await _network.GetActuatorDetails(woNo, serialNo);
        var actuator = new Actuator()
            .WithWorkOrderNumber(woNo)
            .WithSerialNumber(serialNo)
            .WithPCBAUid(networkResponse.PcbaUid);
        return actuator;
    }
}

