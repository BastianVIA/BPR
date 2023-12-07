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

    public async Task<GetActuatorWithFilterResponse> GetActuatorWithFilter(int? woNo, int? serialNo, string? pcbaUid,
        string? itemNo, int? manufacturerNo,
        int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd,
        string? software, string? configNo, string? articleName, string? articleNo, string? comProtocol)
    {
        return await Send(async () =>
            await _client.GetActuatorsWithFilterAsync(woNo, serialNo, pcbaUid, itemNo, manufacturerNo,
                productionDateCode, comProtocol, articleNo, articleName, configNo, software, createdTimeStart,
                createdTimeEnd));
    }

    public async Task<GetTestErrorsWithFilterResponse> GetTestErrorWithFilter(int? wrkOrderNumber, string? tester,
        int? bay, int? errorCode, DateTime startDate, DateTime endDate, int timeIntervalBetweenRowsAsMinutes)
    {
        var sd = await Send(async () =>
            await _client.GetTestErrorsWithFilterAsync(timeIntervalBetweenRowsAsMinutes,startDate,endDate,wrkOrderNumber,tester,bay,errorCode));
        return sd;
    }
    
}