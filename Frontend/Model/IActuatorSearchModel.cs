﻿using Frontend.Entities;

namespace Frontend.Model;

public interface IActuatorSearchModel
{
   Task<List<Actuator>> GetActuatorWithFilter(int? woNo, int? serialNo, string? pcbaUid, string? itemNo, int? manufacturerNo,
        int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd,string? software, string? configNo, string? articleNo, string? comProtocol);
}