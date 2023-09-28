using Frontend.Service;

namespace Frontend.Core;

public interface INetworkAdapter
{
     Task<GetActuatorDetailsResponse> GetActuatorDetails(int workOrderNr, int serialNr);
}