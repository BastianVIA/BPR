using Frontend.Entities;

namespace Frontend.Model;

public interface IActuatorSearchModel
{
    public Task<List<Actuator>> GetActuatorsByPCBA(string uid, int? manufacturerNumber = null);

    public Task<List<Actuator>> SearchActuator(int? woNo, string? uid, int? itemNo, int? manuNo, int? prodDateCode,
        CancellationToken cancellationToken);
}