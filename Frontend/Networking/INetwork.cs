using Frontend.Service;

namespace Frontend.Networking;

public interface INetwork
{
     Task<ConfigurationResponse> GetConfiguration();
     Task<GetActuatorDetailsResponse> GetActuatorDetails(int woNo, int serialNo);
     Task<GetActuatorWithFilterResponse> GetActuatorWithFilter(int? woNo, int? serialNo, string? pcbaUid, string? itemNo, int? manufacturerNo,
         int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd,string? software, string? configNo, string? articleName, string? articleNo, string? comProtocol);
     Task<GetTestErrorForTestersResponse> GetTestErrorForTesters(List<string> testers, TesterTimePeriodEnum timePeriod);
     Task<GetAllTestersResponse> GetAllCellNames();
     Task<GetTestResultsWithFilterResponse> GetTestResultWithFilter(int? woNo, int? serialNo,
         string? tester, int? bay, DateTime? startDate, DateTime? endDate);
     Task<byte[]> GetActuatorWithFilterAsCsv(List<CsvProperties> columnsToInclude, int? woNo, int? serialNo, string? pcbaUid, string? itemNo, int? manufacturerNo,
         int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd,string? software, string? configNo, string? articleName, string? articleNo, string? comProtocol);
}