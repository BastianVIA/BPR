using Frontend.Service;

namespace Frontend.Networking;

public interface INetwork
{
    Task<ConfigurationResponse> GetConfiguration();
    Task<GetActuatorDetailsResponse> GetActuatorDetails(int woNo, int serialNo);

    Task<GetActuatorWithFilterResponse> GetActuatorWithFilter(int? itemNo, int? manufacturerNo,
        int? productionDateCode);
}