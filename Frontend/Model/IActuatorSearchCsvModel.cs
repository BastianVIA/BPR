using Frontend.Service;

namespace Frontend.Model;

public interface IActuatorSearchCsvModel
{
    Task<byte[]> GetActuatorWithFilter(List<CsvProperties> columnsToInclude, int? woNo, int? serialNo, string? pcbaUid, string? itemNo, int? manufacturerNo,
        int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd,string? software, string? configNo, string? articleNo, string? comProtocol);
}