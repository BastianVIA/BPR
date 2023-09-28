using Domain.Entities;

namespace Domain.Repositories;

public interface IActuatorRepository
{
    Task<Actuator> GetActuator(CompositeActuatorId actuatorId);
}