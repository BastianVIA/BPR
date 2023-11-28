using Domain.Entities;
using Infrastructure;

namespace Domain.Repositories;

public interface IActuatorRepository
{
    Task CreateActuator(Actuator actuator);
    Task<Actuator> GetActuator(CompositeActuatorId id);
    Task UpdateActuator(Actuator actuator);
}