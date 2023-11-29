using Frontend.Entities;
using Frontend.Networking;

namespace Frontend.Model;

public class ActuatorSearchModel : IActuatorSearchModel
{
    private INetwork _network;

    public ActuatorSearchModel(INetwork network)
    {
        _network = network;
    }
    public async Task<List<Actuator>> GetActuatorsByPCBA(string uid, int? manufacturerNumber = null)
    {
        var response = await _network.GetActuatorFromPCBA(uid, manufacturerNumber);
         var list = response.Select(entry => new Actuator
         {
             SerialNumber = entry.SerialNumber, 
             WorkOrderNumber = entry.WorkOrderNumber, 
             PCBA =
             {
                 PCBAUid = entry.Uid, 
                 ManufacturerNumber = entry.ManufacturerNumber
             }
         }).ToList();
        return list;
    }
}