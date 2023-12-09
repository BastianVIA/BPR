using Frontend.Service;

namespace Frontend.Networking;

public interface INetwork
{
    public Task<ConfigurationResponse> GetConfiguration();
    public Task<GetActuatorDetailsResponse> GetActuatorDetails(int woNo, int serialNo);
    public Task<GetActuatorWithFilterResponse> GetActuatorWithFilter(int? woNo, int? serialNo, string? pcbaUid, string? itemNo,
        int? manufacturerNo,
        int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd, string? software,
        string? configNo, string? articleNo, string? comProtocol);
    public Task<GetTestErrorForTestersResponse> GetTestErrorForTesters(List<string> testers, TesterTimePeriodEnum timePeriod);
    public Task<GetAllTestersResponse> GetAllCellNames();
    public Task<GetTestResultsWithFilterResponse> GetTestResultWithFilter(int? woNo, int? serialNo,
        string? tester, int? bay, DateTime? startDate, DateTime? endDate);
    public Task UpdateActuatorsPCBA(int woNo, int serialNo, string pcbaUid);
    public Task<GetPCBAChangesForActuatorResponse> GetComponentHistory(int woNo, int serialNo);
    public Task<byte[]> GetActuatorWithFilterAsCsv(List<CsvProperties> columnsToInclude, int? woNo, int? serialNo,
        string? pcbaUid, string? itemNo, int? manufacturerNo,
        int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd, string? software,
        string? configNo, string? articleNo, string? comProtocol);
    public Task<GetTestErrorsWithFilterResponse> GetTestErrorWithFilter(int? woNo, string? tester,
        int? bay, int? errorCode, DateTime startDate, DateTime? endDate, int timeIntervalBetweenRowsAsMinutes);
    public Task<GetStartUpResponse> GetStartUpAmounts();
}