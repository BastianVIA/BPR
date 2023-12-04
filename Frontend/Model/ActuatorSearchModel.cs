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
        int? productionDateCode)
    {
        var networkResponse = await _network.GetActuatorWithFilter(woNo,serialNo, pcbaUid, itemNo, manufacturerNo, productionDateCode);

        var actuators = new List<Actuator>();
        foreach (var responseItem in networkResponse.Actuators)
        {
            var actuator = new Actuator()
                .WithWorkOrderNumber(responseItem.WorkOrderNumber)
                .WithSerialNumber(responseItem.SerialNumber)
                // .WithArticleNumber(responseItem.ArticleNumber)
                // .WithArticleName(responseItem.ArticleName)
                // .WithConfiguration(responseItem.Configuration)
                // .WithCommunicationProtocol(responseItem.CommunicationProtocol)
                // .WithCreatedTime(responseItem.CreatedTime)
                // .WithSoftware(responseItem.Software)
                // .WithConfigNumber(responseItem.ConfigNumber)
                .WithPCBAUid(responseItem.Pcba.PcbaUid)
                .WithPCBAItemNumber(responseItem.Pcba.ItemNumber)
                .WithPCBAManufacturerNumber(responseItem.Pcba.ManufacturerNumber)
                .WithPCBAProductionDateCode(responseItem.Pcba.ProductionDateCode);

            actuators.Add(actuator);
        }

        return actuators;
    }
}