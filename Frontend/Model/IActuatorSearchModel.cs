using Frontend.Entities;

namespace Frontend.Model;

public interface IActuatorSearchModel
{
    public Task<List<Actuator>> GetActuatorsByPCBA(string uid, int? manufacturerNumber = null);

    public Task<List<Actuator>> GetActuatorsWithFilter(string? pcbaUid, string? itemNo, int? manufacturerNo,
        int? productionDateCode);
}