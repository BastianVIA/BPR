using Frontend.Service;

namespace Frontend.Networking;

public interface INetwork
{
     Task<ConfigurationResponse> GetConfiguration();
     Task<GetActuatorDetailsResponse> GetActuatorDetails(int woNo, int serialNo);
     Task<GetActuatorWithFilterResponse> GetActuatorWithFilter(string? pcbaUid,string? itemNo, int? manufacturerNo,
          int? productionDateCode);
     Task<GetActuatorFromPCBAResponse> GetActuatorFromPCBA(string pcbaUid, int? manufacturerNumber = null);

}