﻿
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

    public async Task<GetActuatorDetailsResponse> GetActuatorDetails(int woNo, int serialNo)
    {
        try
        {
            var response = await _client.GetActuatorDetailsAsync(woNo, serialNo);
            _alertService.FireEvent(AlertType.ActuatorDetailsSuccess);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}