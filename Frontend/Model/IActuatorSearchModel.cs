using Frontend.Entities;

namespace Frontend.Model;

public interface IActuatorSearchModel
{
    public Task<List<Actuator>> GetActuatorsByUid(string uid);
}