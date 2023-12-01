using Frontend.Exceptions;
using Frontend.Service;

namespace Frontend.Networking;

public class NSwagProxy : INetwork
{
    private readonly IClient _client;
    public NSwagProxy(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        var uri = configuration.GetSection("BackendApiSettings:Uri").Value;
        var httpClient = clientFactory.CreateClient("BackendApi");
        _client = new Client(uri, httpClient);
    }

    private async Task<T> Send<T>(Func<Task<T>> func)
    {
        try
        {
            return await func();
        }
        catch (ApiException<ProblemDetails> e)
        {
            Console.WriteLine(e);
            throw new NetworkException(e.Result.Detail);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new NetworkException(e.Message);
        }
    }

    public async Task<GetActuatorDetailsResponse> GetActuatorDetails(int woNo, int serialNo)
    {
        return await Send(async () => await _client.GetActuatorDetailsAsync(woNo, serialNo));
    }

    public async Task<ConfigurationResponse> GetConfiguration()
    {
        return await Send(async () => await _client.ConfigurationAsync());
    }

    public async Task<GetActuatorFromPCBAResponse> GetActuatorFromPCBA(string pcbaUid, int? manufacturerNumber = null)
    {
        return await Send(async () => await _client.GetActuatorFromPCBAAsync(pcbaUid, manufacturerNumber));
    }

    public async Task<GetActuatorWithFilterResponse> GetActuatorWithFilter(string? pcbaUid, string? itemNo, int? manufacturerNo,
        int? productionDateCode)
    {
        return await Send(async () =>
            await _client.GetActuatorsWithFilterAsync(pcbaUid, itemNo, manufacturerNo, productionDateCode));
    }
}