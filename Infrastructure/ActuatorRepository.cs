using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure;

public class ActuatorRepository : IActuatorRepository
{
    public async Task<Actuator> GetActuator(CompositeActuatorId actuatorId)
    {
        return Actuator.Create(actuatorId, "testId");
    }
}