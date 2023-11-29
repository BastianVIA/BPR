using Frontend.Entities;

namespace Frontend.Model;

public class ActuatorSearchModel : IActuatorSearchModel
{
    public Task<List<Actuator>> GetActuatorsByUid(string uid)
    {
        throw new NotImplementedException();
    }
}