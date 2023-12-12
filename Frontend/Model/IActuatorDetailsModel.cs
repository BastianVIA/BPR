using Frontend.Entities;

namespace Frontend.Model;

public interface IActuatorDetailsModel
{
    Task<Actuator> GetActuatorDetails(int woNo, int serialNo);
}