using Frontend.Entities;

namespace Frontend.Model;

public interface IGetActuatorsWithFilterModel
{
    public Task<List<Actuator>> GetActuatorWithFilter(string? pcbaUid, string? itemNo, int? manufacturerNo,
        int? productionDateCode);

}