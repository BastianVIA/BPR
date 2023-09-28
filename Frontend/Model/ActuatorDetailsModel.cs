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

    public async Task<Actuator> GetActuatorDetails(string woNo, string serialNo)
    {
        var networkResponse = await _network.GetActuatorDetails(woNo, serialNo);
        var toReturn = toEntity(networkResponse);
        Console.WriteLine(toReturn.PcbaId);
        return toReturn;
    }

    public Actuator toEntity(GetActuatorDetailsResponse response)
    {
        return new Actuator(response.PcbaId);
    }
    
    
}

