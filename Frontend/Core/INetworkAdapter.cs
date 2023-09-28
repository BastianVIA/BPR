using Frontend.Service;

namespace Frontend.Core;

public interface INetworkAdapter
{
     Task<GetActuatorDetailsResponse> GetActuatorDetails(string workOrderNr, string serialNr);
}