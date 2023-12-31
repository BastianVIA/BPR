﻿using Frontend.Exceptions;
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

    private async Task Send(Func<Task> func)
    {
        try
        {
            await func();
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
        return await Send(async () => await _client.GetConfigurationAsync());
    }

    public async Task<GetActuatorWithFilterResponse> GetActuatorWithFilter(int? woNo, int? serialNo, string? pcbaUid,
        string? itemNo, int? manufacturerNo, int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd,
        string? software, string? configNo, string? articleNo, string? comProtocol)
    {
        return await Send(async () =>
            await _client.GetActuatorsWithFilterAsync(woNo, serialNo, pcbaUid, itemNo, manufacturerNo,
                productionDateCode, comProtocol, articleNo, configNo, software, createdTimeStart,
                createdTimeEnd));
    }

    public async Task<GetTestResultsWithFilterResponse> GetTestResultWithFilter(int? woNo, int? serialNo,
        string? tester, int? bay, DateTime? startDate, DateTime? endDate)
    {
        return await Send(async () =>
            await _client.GetTestResultsWithFilterAsync(woNo, serialNo, tester, bay, startDate, endDate));
    }

    public async Task<byte[]> GetActuatorWithFilterAsCsv(List<CsvProperties> columnsToInclude, int? woNo, int? serialNo,
        string? pcbaUid, string? itemNo,
        int? manufacturerNo, int? productionDateCode, DateTime? createdTimeStart, DateTime? createdTimeEnd,
        string? software, string? configNo, string? articleNo, string? comProtocol)
    {
        var response = await Send(async () =>
            await _client.GetActuatorWithFilterAsCsvAsync(woNo, serialNo, pcbaUid, itemNo, manufacturerNo,
                productionDateCode, comProtocol, articleNo, configNo, software, createdTimeStart, createdTimeEnd,
                columnsToInclude));

        using MemoryStream memoryStream = new();
        await response.Stream.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public async Task<GetTestErrorForTestersResponse> GetTestErrorForTesters(List<string> testers,
        TesterTimePeriodEnum timePeriod)
    {
        return await Send(async () => await _client.GetTestErrorForTestersAsync(testers, timePeriod));
    }

    public async Task<GetAllTestersResponse> GetAllCellNames()
    {
        return await Send(async () => await _client.GetAllTestersAsync());
    }

    public async Task<GetTestErrorsWithFilterResponse> GetTestErrorWithFilter(int? wrkOrderNumber, string? tester,
        int? bay, int? errorCode, DateTime startDate, DateTime? endDate, int timeIntervalBetweenRowsAsMinutes)
    {
        var sd = await Send(async () =>
            await _client.GetTestErrorsWithFilterAsync(timeIntervalBetweenRowsAsMinutes, startDate, endDate,
                wrkOrderNumber, tester, bay, errorCode));
        return sd;
    }

    public async Task<GetStartUpResponse> GetStartUpAmounts()
    {
        return await Send(async () => await _client.GetStartUpAmountsAsync());
    }

    public async Task UpdateActuatorsPCBA(int woNo, int serialNo, string pcbaUid)
    {
        PutActuatorPCBARequest body = new PutActuatorPCBARequest
        {
            WorkOrderNumber = woNo,
            SerialNumber = serialNo,
            PcbaUid = pcbaUid
        };
        await Send(async () => await _client.PutNewPCBAInActuatorAsync(body));
    }

    public async Task<GetPCBAChangesForActuatorResponse> GetComponentHistory(int woNo, int serialNo)
    {
        return await Send(async () => await _client.GetPCBAChangesForActuatorAsync(woNo, serialNo));
    }
}