using AutoFixture;
using Backend.Controllers.TestResult;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using TestResult.Application.GetAllTesters;

namespace Backend.Tests.Controllers.TestResult;

public class GetAllTestersControllerTests
{
    private Fixture _fixture = new();
    private GetAllTestersController _controller;
    private readonly IQueryBus _bus = Substitute.For<IQueryBus>();

    public GetAllTestersControllerTests()
    {
        _controller = new GetAllTestersController(_bus);
    }

    [Fact]
    public async Task GetAsync_ReturnsOk_WhenNoTesterExists()
    {
        //Arrange
        var dto = GetAllTestersDto.From(new List<string>());

        _bus.Send(Arg.Any<GetAllTestersQuery>(), CancellationToken.None).Returns(dto);
        
        //Act
        var result = await _controller.GetAsync(CancellationToken.None);
        
        //Assert
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async Task GetAsync_ReturnsOk_WhenOneTesterExists()
    {
        //Arrange
        var dto = GetAllTestersDto.From(new List<string>(){"Tester1"});

        _bus.Send(Arg.Any<GetAllTestersQuery>(), CancellationToken.None).Returns(dto);
        
        //Act
        var result = await _controller.GetAsync(CancellationToken.None);
        
        //Assert
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async Task GetAsync_ReturnsOk_WhenManyTesterExists()
    {
        //Arrange
        var dto = GetAllTestersDto.From(new List<string>(){"Tester1", "Tester2","Tester3"});

        _bus.Send(Arg.Any<GetAllTestersQuery>(), CancellationToken.None).Returns(dto);
        
        //Act
        var result = await _controller.GetAsync(CancellationToken.None);
        
        //Assert
        Assert.IsType<OkObjectResult>(result);
    }
}