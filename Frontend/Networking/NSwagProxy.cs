using Frontend.Events;
using Frontend.Service;

namespace Frontend.Networking;

public class NSwagProxy : INetworkAdapter
{
    private readonly Client _client;
    private IAlertService _alertService;

    public NSwagProxy(IHttpClientFactory clientFactory, IConfiguration configuration, IAlertService alertService)
    {
        var uri = configuration.GetSection("BackendApiSettings:Uri").Value;
        var httpClient = clientFactory.CreateClient("BackendApi");
        _client = new Client(uri, httpClient);
        _alertService = alertService;
    }
    
    public async Task<GetActuatorDetailsResponse?> GetActuatorDetails(int woNo, int serialNo)
    {
        GetActuatorDetailsResponse? response = null;
        try
        {
            response = await _client.GetActuatorDetailsAsync(woNo, serialNo);
        }
        catch (ApiException e)
        {
            Console.WriteLine(e);
            _alertService.FireEvent(e.StatusCode == 404 ? AlertType.ActuatorDetailsFailure : AlertType.NetworkError);
        }
        return response;
    }
}