using Frontend.Core;
using Frontend.Entities;
using Frontend.Service;

namespace Frontend.Model;

public class ActuatorDetailsModel : IActuatorDetailsModel
{
    private INetworkAdapter _network;
    private Actuator actuator;
    
    public ActuatorDetailsModel(INetworkAdapter network)
    {
        _network = network;
    }

    public async Task<Actuator> GetActuatorDetails(int woNo, int serialNo)
    {
        actuator = new Actuator { WorkOrderNumber = woNo, SerialNumber = serialNo, PCBA = new PCBA()};
        var networkResponse = await _network.GetActuatorDetails(woNo, serialNo);
        UpdateActuatorWithResponse(networkResponse);
        return actuator;
    }

    public void UpdateActuatorWithResponse(GetActuatorDetailsResponse response)
    {
        actuator.PCBA.PCBAUid = Int32.Parse(response.PcbaId);
    }
}

