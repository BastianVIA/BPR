using Domain.Entities;

namespace Domain.Repositories;

public interface IActuatorPCBAHistory
{
    Task<List<ActuatorPCBAChange>> GetPCBAChangesForActuator(int woNo, int serialNo);
    Task PCBARemoved(ActuatorPCBAChange change);
}