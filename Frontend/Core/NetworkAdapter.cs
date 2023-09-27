using Frontend.Service;

namespace Frontend.Core;

public class NSwagAdapter : INetworkAdapter
{
    private Client _client;
    
    public NSwagAdapter(Client client)
    {
        _client = client;
    }

    public Task<T> GetActuatorDetails<T>(int workOrderNr, int serialNr)
    {
        var response = new Task<T>(null);
        
        
        _client.PostAsync()
        return response;
    }
}