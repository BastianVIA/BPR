using Frontend.Entities;
using Frontend.NSwagServiceAdapter;

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
        var actuator = new Actuator();
        var networkResponse = await _network.GetActuatorDetails(woNo, serialNo);
        if (networkResponse is null)
        {
            return actuator;
        }

        actuator
            .WithWorkOrderNumber(woNo)
            .WithSerialNumber(serialNo)
            .WithPCBAUid(networkResponse.PcbaUid);
        return actuator;
    }
}

