using Frontend.Service;

namespace Frontend.Networking;

public interface INetworkAdapter
{
     Task<GetActuatorDetailsResponse?> GetActuatorDetails(int woNo, int serialNo);
}