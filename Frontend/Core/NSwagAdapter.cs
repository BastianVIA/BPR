
using Frontend.Events;
using Frontend.Model;
using Frontend.Service;

namespace Frontend.Core;

public class NSwagAdapter : INetworkAdapter
{
    private readonly Client _client;
    private IAlertService _alertService;

    public NSwagAdapter(IHttpClientFactory clientFactory, IConfiguration configuration, IAlertService alertService)
    {
        var uri = configuration.GetSection("BackendApiSettings:Uri").Value;
        var httpClient = clientFactory.CreateClient("BackendApi");
        _client = new Client(uri, httpClient);
        _alertService = alertService;
    }

    private async Task<T> Send<T>(Func<Task<T>> action)
    {
        T response = default!;
        try
        {
            response = await action();
        }
        catch (ApiException e)
        {
            Console.WriteLine(e);
            _alertService.FireEvent(e.StatusCode == 404 ? AlertType.ActuatorDetailsFailure : AlertType.NetworkError);
        }
        return response;
    }

    public async Task<GetActuatorDetailsResponse?> GetActuatorDetails(int woNo, int serialNo)
    {
        var response = await Send(async () => await _client.GetActuatorDetailsAsync(woNo, serialNo));
        return response;
    }
    
}