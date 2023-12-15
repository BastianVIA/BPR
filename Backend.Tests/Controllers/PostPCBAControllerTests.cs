using Application.CreatePCBA;
using Backend.Controllers;
using Backend.Controllers.PCBA;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Backend.Tests.Controllers;

public class PostPCBAControllerTests
{
    private PostPCBAController _controller;
    private readonly ICommandBus _bus = Substitute.For<ICommandBus>();
    
    public PostPCBAControllerTests()
    {
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
        _bus.Send(Arg.Any<CreateOrUpdatePCBACommand>(), Arg.Any<CancellationToken>()).ThrowsAsync<Exception>();

        var request = new PostPCBARequest();
        var result = await _controller.CreateAsync(request, CancellationToken.None);
        Assert.IsType<BadRequestResult>(result);
    }
    
}