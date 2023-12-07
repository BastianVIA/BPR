using Frontend.Entities;

namespace Frontend.Model;

public interface IActuatorComponentHistoryModel
{
    public Task<List<ComponentChange>> GetComponentHistory(int woNo, int serialNo);
}