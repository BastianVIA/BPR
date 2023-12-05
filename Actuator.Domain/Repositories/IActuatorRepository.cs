using Domain.Entities;

namespace Domain.Repositories;

public interface IActuatorRepository
{
    Task CreateActuator(Actuator actuator);
    Task<Actuator> GetActuator(CompositeActuatorId id);
    Task UpdateActuator(Actuator actuator);
    Task<List<Actuator>> GetActuatorsWithFilter(int? woNo, int? serialNo, string? pcbaUid, string? pcbaItemNumber, int? pcbaManufacturerNumber, int? pcbaProductionDateCode);
    Task<List<Actuator>> GetActuatorsFromPCBAAsync(string requestUid, int? requestManufacturerNo);
}