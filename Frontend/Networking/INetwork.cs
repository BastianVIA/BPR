using Frontend.Service;

namespace Frontend.Networking;

public interface INetwork
{
     Task<GetActuatorDetailsResponse?> GetActuatorDetails(int woNo, int serialNo);
}