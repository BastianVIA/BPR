
using Frontend.Model;
using Frontend.Service;

namespace Frontend.Core;

public class NSwagAdapter : INetworkAdapter
{
    private readonly Client _client;

    public NSwagAdapter(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        var uri = configuration.GetSection("BackendApiSettings:Uri").Value;
        var httpClient = clientFactory.CreateClient("BackendApi");
        _client = new Client(uri, httpClient);
    }

    public async Task<GetActuatorDetailsResponse> GetActuatorDetails(int workOrderNr, int serialNr)
    {
        //var response = await _client.GetActuatorDetailsAsync(workOrderNr, serialNr);
        //return response;
        Console.WriteLine("does it works: " + _client.BaseUrl);
        return new GetActuatorDetailsResponse() { PcbaId = "1234"};
    }
    
    
}