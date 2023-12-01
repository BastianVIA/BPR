using Frontend.Entities;
using Frontend.Networking;

namespace Frontend.Model;

public class ActuatorDetailsModel : IActuatorDetailsModel
{
    private readonly INetwork _network;
    public ActuatorDetailsModel(INetwork network)
    {
        _network = network;
    }
    
    public async Task<Actuator> GetActuatorDetails(int woNo, int serialNo)
    {
        var networkResponse = await _network.GetActuatorDetails(woNo, serialNo);
        return new Actuator()
            .WithWorkOrderNumber(woNo)
            .WithSerialNumber(serialNo)
            .WithPCBAUid(networkResponse.Pcba.Uid)
            .WithPCBAManufacturerNumber(networkResponse.Pcba.ManufacturerNumber);
    }
}

