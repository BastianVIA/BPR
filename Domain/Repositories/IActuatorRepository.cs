using Domain.Entities;
using Infrastructure;

namespace Domain.Repositories;

public interface IActuatorRepository
{
    Task CreateActuator(Actuator actuator);
    Task<Actuator> GetActuator(CompositeActuatorId id);
    Task UpdateActuator(Actuator actuator);
    Task<List<Actuator>> GetActuatorsWithFilter( string requestUid, string? requestItemNumber, int? requestManufacturerNumber, int? requestProductionDateCode);
    Task<List<Actuator>> GetActuatorsFromPCBAAsync(string requestUid, int? requestManufacturerNo);
}