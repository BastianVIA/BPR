﻿using Frontend.Services;

namespace Frontend.Model;

public interface IActuatorSearchCsvModel
{
    public Task<byte[]> GetActuatorWithFilter(List<CsvProperties> columnsToInclude, int? woNo, int? serialNo, string? pcbaUid, string? itemNo, int? manufacturerNo,
        int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd,string? software, string? configNo, string? articleName, string? articleNo, string? comProtocol);
}