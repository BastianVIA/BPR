using Frontend.Service;

namespace Frontend.NSwagServiceAdapter;

public interface INetworkAdapter
{
     Task<GetActuatorDetailsResponse?> GetActuatorDetails(int woNo, int serialNo);
}