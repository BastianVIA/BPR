using System.Reflection;
using Frontend.Exceptions;
using Frontend.Networking;
using Frontend.Service;
using Microsoft.Extensions.Configuration;
using NSubstitute.ExceptionExtensions;

namespace Frontend.UnitTest.Network;

public class NSwagProxyTests
{
    private Fixture _fixture = new();
    private IHttpClientFactory _clientFactory = Substitute.For<IHttpClientFactory>();
    private IConfiguration _configuration = Substitute.For<IConfiguration>();
    private IClient _client = Substitute.For<IClient>();

    private NSwagProxy _network;
    
    public NSwagProxyTests()
    {
        _network = new NSwagProxy(_clientFactory, _configuration);

        var clientField = _network.GetType().GetField("_client", BindingFlags.Instance | BindingFlags.NonPublic);
        clientField!.SetValue(_network,_client);
    }

    [Fact]
    public async Task GetActuatorFromPCBA_ReturnsListOfGetActuatorFromPCBAActuator_OnSuccess()
    {
        var request = _fixture.Create<string>();
        var expectedList = new List<GetActuatorFromPCBAActuator>();

        _client.GetActuatorFromPCBAAsync(Arg.Any<string>(), Arg.Any<int?>())
            .Returns(expectedList);
        
        var result= await _network.GetActuatorFromPCBA(request);
        Assert.NotNull(result);
        Assert.IsType<List<GetActuatorFromPCBAActuator>>(result);
    }

    [Fact]
    public async Task GetActuatorFromPCBA_ThrowsNetworkException_OnApiException()
    {
        var request = _fixture.Create<string>();
        
        _client.GetActuatorFromPCBAAsync(Arg.Any<string>(), Arg.Any<int?>())
            .ThrowsAsync<ApiException>();

        await Assert.ThrowsAsync<NetworkException>(() => _network.GetActuatorFromPCBA(request));
    }

    [Fact]
    public async Task GetActuatorFromPCBA_ThrowsNetworkException_OnException()
    {
        var request = _fixture.Create<string>();
        
        _client.GetActuatorFromPCBAAsync(Arg.Any<string>(), Arg.Any<int?>())
            .ThrowsAsync<Exception>();

        await Assert.ThrowsAsync<NetworkException>(() => _network.GetActuatorFromPCBA(request));
    }
}