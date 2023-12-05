using Frontend.Networking;
using Frontend.Service;

namespace Frontend.Model;

public class ActuatorSearchCsvModel : IActuatorSearchCsvModel
{
    private INetwork _network;

    public ActuatorSearchCsvModel(INetwork network)
    {
        _network = network;
    }

    public async Task<byte[]> GetActuatorWithFilter(List<string> columnsToInclude, int? woNo, int? serialNo,
        string? pcbaUid, string? itemNo,
        int? manufacturerNo, int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd,
        string? software, string? configNo, string? articleName, string? articleNo, string? comProtocol)
    {
        /*
        columnsToInclude = new List<string>()
        {
            "WorkOrderNumber",
            "SerialNumber",
            "CommunicationProtocol",
            "ArticleNumber",
            "ArticleName",
            "CreatedTime",
            "PCBAUid",
            "PCBAManufacturerNumber",
            "PCBAItemNumber",
            "PCBASoftware",
            "PCBAProductionDateCode",
            "PCBAConfigNo",
        };
        */
        List<CsvProperties> propertiesList = new();
        foreach (var column in columnsToInclude)
        {
            CsvProperties csvProp = (CsvProperties)Enum.Parse(typeof(CsvProperties), column);
            propertiesList.Add(csvProp);
        }

        var networkResponse = await _network.GetActuatorWithFilterAsCsv(propertiesList, woNo, serialNo, pcbaUid, itemNo,
            manufacturerNo,
            productionDateCode, createdTimeStart, createdTimeEnd, software, configNo, articleName, articleNo,
            comProtocol);
        return networkResponse;
    }
}