using Application.CreatePCBA;
using Backend.Controllers;
using Backend.Controllers.PCBA;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Backend;

namespace Backend.Tests.Controllers;

public class PostPCBAControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private PostPCBAController _controller;
    private readonly ICommandBus _bus = Substitute.For<ICommandBus>();
    
    public PostPCBAControllerTests(WebApplicationFactory<Program> application)
    {
        _client = application.CreateClient();
        _controller = new PostPCBAController(_bus);
    }
    
    [Fact]
    public async Task CreateAsync_ReturnsOK_WhenPCBACreated()
    {
        var request = new PostPCBARequest{Uid = "123"};
        var result = await _controller.CreateAsync(request, CancellationToken.None);
        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task CreateAsync_ReturnsBadRequest_WhenPCBACreationFailed()
    {
        // Arrange
        var request = new PostPCBARequest();
        //_bus.Send(Arg.Any<CreateOrUpdatePCBACommand>(), Arg.Any<CancellationToken>()).ThrowsAsync<Exception>();
        _bus.When(x => x.Send(Arg.Any<CreateOrUpdatePCBACommand>(), Arg.Any<CancellationToken>()))
            .Do(_ => throw new ArgumentException());
        // Act
        
        var result = await _controller.CreateAsync(request, CancellationToken.None);
        // Assert
        
        
        Assert.IsType<BadRequestResult>(result);
    }
    
}