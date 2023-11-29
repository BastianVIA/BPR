using Frontend.Entities;

namespace Frontend.Model;

public interface IActuatorDetailsModel
{
    public Task<Actuator> GetActuatorDetails(int woNo, int serialNo);
    public Task<List<Actuator>> GetActuatorsByUid(string uid);
}