using Frontend.Entities;

namespace Frontend.Model;

public interface IActuatorSearchModel
{
   public Task<List<Actuator>> GetActuatorWithFilter(int? woNo, int? serialNo, string? pcbaUid, string? itemNo, int? manufacturerNo,
        int? productionDateCode);
}