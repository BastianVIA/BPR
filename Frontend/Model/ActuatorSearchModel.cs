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

    public async Task<List<Actuator>> GetActuatorWithFilter(int? woNo, int? serialNo, string? pcbaUid, string? itemNo, int? manufacturerNo,
        int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd,string? software, string? configNo, string? articleNo, string? comProtocol)
    {
        var networkResponse = await _network.GetActuatorWithFilter(woNo, serialNo, pcbaUid, itemNo, manufacturerNo,
            productionDateCode, createdTimeStart, createdTimeEnd,software,configNo,articleNo,comProtocol);

        var actuators = new List<Actuator>();
        foreach (var responseItem in networkResponse.Actuators)
        {
            var actuator = new Actuator()
                .WithWorkOrderNumber(responseItem.WorkOrderNumber)
                .WithSerialNumber(responseItem.SerialNumber)
                .WithArticleNumber(responseItem.ArticleNumber)
                .WithCommunicationProtocol(responseItem.CommunicationProtocol)
                .WithCreatedTime(responseItem.CreatedTime)
                .WithSoftware(responseItem.Pcba.Software)
                .WithConfigNumber(responseItem.Pcba.ConfigNo)
                .WithPCBAUid(responseItem.Pcba.Uid)
                .WithPCBAItemNumber(responseItem.Pcba.ItemNumber)
                .WithPCBAManufacturerNumber(responseItem.Pcba.ManufacturerNumber)
                .WithPCBAProductionDateCode(responseItem.Pcba.ProductionDateCode);

            actuators.Add(actuator);
        }

        return actuators;
    }
}