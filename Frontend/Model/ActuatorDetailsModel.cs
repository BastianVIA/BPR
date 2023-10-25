using Frontend.Entities;
using Frontend.Networking;
using Frontend.Service;

namespace Frontend.Model;

public class ActuatorDetailsModel : IActuatorDetailsModel
{
    private INetwork _network;

    public ActuatorDetailsModel(INetwork network)
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

