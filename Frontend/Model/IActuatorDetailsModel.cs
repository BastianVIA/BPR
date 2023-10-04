using Frontend.Entities;
using Frontend.Service;

namespace Frontend.Model;

public interface IActuatorDetailsModel
{
    public Task<Actuator> GetActuatorDetails(int woNo, int serialNo);
   
}