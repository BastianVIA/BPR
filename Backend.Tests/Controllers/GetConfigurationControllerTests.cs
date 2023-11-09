using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Backend.Controllers;
using Microsoft.Extensions.Configuration;
using Moq;
using NSubstitute;

namespace Backend.Tests.Controllers;

public class GetConfigurationControllerTests
{
    private Fixture _fixture = new();
    private MockConfiguration _configuration = new();
    
    private GetConfigurationController _controller;

    public GetConfigurationControllerTests()
    {
        _fixture.Customize(new AutoNSubstituteCustomization());
        //_configuration = _fixture.Freeze<IConfiguration>();
        _controller = new GetConfigurationController(_configuration);
    }

    [Fact]
    public async Task GetAsync_ReturnsConfigurationResponse_WhenCalled()
    {
        var expectedWoNoLength = _fixture.Create<int>();
        var expectedSerialNumberMinLength = _fixture.Create<int>();
        var expectedSerialNumberMaxLength = _fixture.Create<int>();

        
        

        
        var result = _controller.GetAsync(CancellationToken.None);
        Console.WriteLine();
        
    }
}