using Application.CreatePCBA;
using Backend.Controllers;
using Backend.Controllers.PCBA;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;


namespace Backend.Tests.Controllers;

public class PostPCBAControllerTests 
{
    private readonly HttpClient _client;
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

}