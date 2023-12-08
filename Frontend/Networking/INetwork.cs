using Frontend.Service;

namespace Frontend.Networking;

public interface INetwork
{
     Task<ConfigurationResponse> GetConfiguration();
     Task<GetActuatorDetailsResponse> GetActuatorDetails(int woNo, int serialNo);
     Task<GetActuatorWithFilterResponse> GetActuatorWithFilter(int? woNo, int? serialNo, string? pcbaUid, string? itemNo, int? manufacturerNo,
         int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd,string? software, string? configNo, string? articleName, string? articleNo, string? comProtocol);
     Task<GetTestResultsWithFilterResponse> GetTestResultWithFilter(int? woNo, int? serialNo,
         string? tester, int? bay);
     Task<byte[]> GetActuatorWithFilterAsCsv(List<CsvProperties> columnsToInclude, int? woNo, int? serialNo, string? pcbaUid, string? itemNo, int? manufacturerNo,
         int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd,string? software, string? configNo, string? articleName, string? articleNo, string? comProtocol);
     Task UpdateActuatorsPCBA(int woNo, int serialNo, string pcbaUid);
     Task<GetPCBAChangesForActuatorResponse> GetComponentHistory(int woNo, int serialNo);
}