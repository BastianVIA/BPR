using Frontend.Entities;
using Frontend.Networking;

namespace Frontend.Model;

public class ActuatorDetailsModel : IActuatorDetailsModel
{
    private INetwork _network;
    public ActuatorDetailsModel(INetwork network)
    {
        _network = network ?? throw new ArgumentNullException(nameof(network));
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

