using Frontend.Entities;

namespace Frontend.Model;

public interface IActuatorComponentHistoryModel
{
    Task<List<ComponentChange>> GetComponentHistory(int woNo, int serialNo);
}