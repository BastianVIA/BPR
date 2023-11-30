using Frontend.Entities;

namespace Frontend.Model;

public interface IGetActuatorsWithFilterModel
{
    public Task<List<Actuator>> GetActuatorWithFilter(int? itemNo, int? manufacturerNo,
        int? productionDateCode);

}