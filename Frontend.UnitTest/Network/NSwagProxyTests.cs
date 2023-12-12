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
    public async Task GetActuatorDetails_ReturnsGetActuatorResponse_OnSuccess()
    {
        // Arrange
        var woNo = _fixture.Create<int>();
        var serialNumber = _fixture.Create<int>();

        var expected = _fixture.Create<GetActuatorDetailsResponse>();

        _client.GetActuatorDetailsAsync(Arg.Any<int>(), Arg.Any<int>())
            .Returns(expected);
        
        // Act
        var result = await _network.GetActuatorDetails(woNo, serialNumber);
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<GetActuatorDetailsResponse>(result);
    }
    
    [Fact]
    public async Task GetActuatorDetails_ThrowsNetworkException_OnApiException()
    {
        // Arrange
        var woNo = _fixture.Create<int>();
        var serialNumber = _fixture.Create<int>();

        _client.GetActuatorDetailsAsync(Arg.Any<int>(), Arg.Any<int>())
            .ThrowsAsync<ApiException>();
        
        // Act/Assert
        await Assert.ThrowsAsync<NetworkException>(() => _network.GetActuatorDetails(woNo, serialNumber));
    }
    
    [Fact]
    public async Task GetActuatorDetails_ThrowsNetworkException_OnException()
    {
        // Arrange
        var woNo = _fixture.Create<int>();
        var serialNumber = _fixture.Create<int>();

        _client.GetActuatorDetailsAsync(Arg.Any<int>(), Arg.Any<int>())
            .ThrowsAsync<Exception>();

        // Act/Assert
        await Assert.ThrowsAsync<NetworkException>(() => _network.GetActuatorDetails(woNo, serialNumber));
    }

    [Fact]
    public async Task GetConfiguration_ReturnsGetConfigurationResponse_OnSuccess()
    {
        // Arrange
        var expected = _fixture.Create<ConfigurationResponse>();

        _client.GetConfigurationAsync(Arg.Any<CancellationToken>())
            .Returns(expected);
        
        // Act
        var result = await _network.GetConfiguration();
        
        // Assert
        Assert.NotNull(result);
        Assert.IsType<ConfigurationResponse>(result);
    }
    
    [Fact]
    public async Task GetConfiguration_ThrowsNetworkException_OnApiException()
    {
        // Arrange
        var woNo = _fixture.Create<int>();
        var serialNumber = _fixture.Create<int>();

        _client.GetActuatorDetailsAsync(Arg.Any<int>(), Arg.Any<int>())
            .ThrowsAsync<ApiException>();

        // Act/Assert
        await Assert.ThrowsAsync<NetworkException>(() => _network.GetActuatorDetails(woNo, serialNumber));
    }
    
    [Fact]
    public async Task GetConfiguration_ThrowsNetworkException_OnException()
    {
        // Arrange
        var woNo = _fixture.Create<int>();
        var serialNumber = _fixture.Create<int>();

        _client.GetActuatorDetailsAsync(Arg.Any<int>(), Arg.Any<int>())
            .ThrowsAsync<Exception>();
        
        // Act/Assert
        await Assert.ThrowsAsync<NetworkException>(() => _network.GetActuatorDetails(woNo, serialNumber));
    }
    
    [Fact]
    public async Task GetActuatorsWithFilter_ThrowsNetworkException_WhenGivenNoSearchParameters()
    {
        // Arrange
        _client.GetActuatorsWithFilterAsync(Arg.Any<int?>(),
                Arg.Any<int?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<int?>(),
                Arg.Any<int?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<DateTime?>(),
                Arg.Any<DateTime?>())
            .ThrowsAsync<ApiException>();

        // Act/Assert
        await Assert.ThrowsAsync<NetworkException>(() => _network.GetActuatorWithFilter(null, null, null, null, null, null, null, null, null, null, null, null));
    }
}