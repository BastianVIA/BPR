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

    public async Task<byte[]> GetActuatorWithFilter(List<CsvProperties> columnsToInclude, int? woNo, int? serialNo,
        string? pcbaUid, string? itemNo,
        int? manufacturerNo, int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd,
        string? software, string? configNo, string? articleName, string? articleNo, string? comProtocol)
    {
        var networkResponse = await _network.GetActuatorWithFilterAsCsv(columnsToInclude, woNo, serialNo, pcbaUid, itemNo,
            manufacturerNo,
            productionDateCode, createdTimeStart, createdTimeEnd, software, configNo, articleNo,
            comProtocol);
        return networkResponse;
    }
}