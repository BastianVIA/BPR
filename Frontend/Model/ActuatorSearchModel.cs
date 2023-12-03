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
         var list = response.Actuators.Select(entry => new Actuator
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

    public async Task<List<Actuator>> SearchActuator(int? woNo, string? uid, int? itemNo, int? manuNo, int? prodDateCode, CancellationToken cancellationToken)
    {
        var list = new List<Actuator>();

        return list;
    }
    
    public async Task<List<Actuator>> GetActuatorsWithFilter(string? pcbaUid, string? itemNo, int? manufacturerNo, int? productionDateCode)
    {
        var networkResponse = await _network.GetActuatorWithFilter( pcbaUid, itemNo, manufacturerNo, productionDateCode);

        var actuators = new List<Actuator>();
        foreach (var responseItem in networkResponse.Actuators)
        {
            var actuator = new Actuator()
                .WithWorkOrderNumber(responseItem.WorkOrderNumber)
                .WithSerialNumber(responseItem.SerialNumber)
                .WithPCBAUid(responseItem.Pcba.PcbaUid)
                .WithPCBAItemNumber(responseItem.Pcba.ItemNumber)
                .WithPCBAManufacturerNumber(responseItem.Pcba.ManufacturerNumber)
                .WithPCBAProductionDateCode(responseItem.Pcba.ProductionDateCode);

            actuators.Add(actuator);
        }

        return actuators;
    }
}