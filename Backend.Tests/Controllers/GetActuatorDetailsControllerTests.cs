using Application.GetActuatorDetails;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using PolymorphicContracts;
using Backend.Controllers;
using BuildingBlocks.Application;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using PolymorphicContracts.AutoFixture;

namespace Backend.Tests.Controllers;

public class GetActuatorDetailsControllerTests
{
    private Fixture _fixture = new();
    private GetActuatorDetailsController _controller;
    private readonly IQueryBus _bus = Substitute.For<IQueryBus>();
    
    public GetActuatorDetailsControllerTests()
    {
        _controller = new GetActuatorDetailsController(_bus);
    }
    
    [Fact]
    public async Task GetAsync_ReturnsOK_WhenActuatorExists()
    {
        var woNo = _fixture.Create<int>();
        var serialNo = _fixture.Create<int>();
        var actuator = _fixture.Create<Actuator>();

        var dto  = GetActuatorDetailsDto.From(actuator);

        _bus.Send(Arg.Any<GetActuatorDetailsQuery>(), Arg.Any<CancellationToken>())
            .Returns(dto);

        var result = await _controller.GetAsync(woNo, serialNo, CancellationToken.None);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetAsync_Returns404_WhenActuatorNotFound()
    {
        var woNo = _fixture.Create<int>();
        var serialNo = _fixture.Create<int>();
        
        _bus.Send(Arg.Any<GetActuatorDetailsQuery>(), Arg.Any<CancellationToken>())
            .ThrowsAsync<KeyNotFoundException>();

        
        await Assert.ThrowsAsync<KeyNotFoundException>(() => 
            _controller.GetAsync(woNo, serialNo, CancellationToken.None));
    }
    
    
}
