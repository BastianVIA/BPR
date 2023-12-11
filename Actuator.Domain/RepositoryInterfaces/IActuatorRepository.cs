using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IActuatorRepository
{
    Task CreateActuator(Actuator actuator);
    Task<Actuator> GetActuator(CompositeActuatorId id);
    Task UpdateActuator(Actuator actuator);
    Task<List<Actuator>> GetActuatorsWithFilter(int? woNo, int? serialNo, string? pcbaUid, string? pcbaItemNumber, int? pcbaManufacturerNumber, int? pcbaProductionDateCode, string? communicationProtocol, string? articleNumber, string? configNo, string? software, DateTime? startDate, DateTime? endDate);
    Task<List<Actuator>> GetActuatorsFromPCBAAsync(string requestUid, int? requestManufacturerNo);
    Task<int> GetTotalActuatorAmount();
}