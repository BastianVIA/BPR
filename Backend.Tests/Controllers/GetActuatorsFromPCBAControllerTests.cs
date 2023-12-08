using Application.GetActuatorFromPCBA;
using AutoFixture;
using Backend.Controllers;
using Backend.Controllers.Actuator;
using BuildingBlocks.Application;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NuGet.Frameworks;

namespace Backend.Tests.Controllers;

public class GetActuatorsFromPCBAControllerTests
{
    private Fixture _fixture = new();
    private GetActuatorFromPCBAController _controller;
    private readonly IQueryBus _bus = Substitute.For<IQueryBus>();

    public GetActuatorsFromPCBAControllerTests()
    {
        _controller = new GetActuatorFromPCBAController(_bus);
    }

    [Fact]
    public async Task GetAsync_ReturnsOkAndActuator_WhenActuatorsAreFound()
    {
        //Arrange
        var noOfFoundActuators = 1;
        var uid = _fixture.Create<string>();

        var actuators = _fixture.CreateMany<Actuator>(noOfFoundActuators).ToList();

        var dto = GetActuatorFromPCBADto.From(actuators);
        
        //Act
        _bus.Send(Arg.Any<GetActuatorFromPCBAQuery>(), Arg.Any<CancellationToken>()).Returns(dto);
        
        //Assert
        var result = await _controller.GetAsync(uid, null, CancellationToken.None) as ObjectResult;
        Assert.IsType<OkObjectResult>(result);
        
        Assert.NotNull(result.Value);
        Assert.IsAssignableFrom<IEnumerable<GetActuatorFromPCBAActuator>>(result.Value);
        List<GetActuatorFromPCBAActuator> resultAsEnumerable = (List<GetActuatorFromPCBAActuator>)result.Value;
        Assert.NotEmpty(resultAsEnumerable);
        
        Assert.Equal(dto.Actuators[0].WorkOrderNumber, resultAsEnumerable[0].WorkOrderNumber);
        Assert.Equal(dto.Actuators[0].SerialNumber, resultAsEnumerable[0].SerialNumber);
        Assert.Equal(dto.Actuators[0].Uid, resultAsEnumerable[0].Uid);
        Assert.Equal(dto.Actuators[0].ManufacturerNumber, resultAsEnumerable[0].ManufacturerNumber);
    }

    [Fact]
    public async Task GetAsync_ReturnOkAndEmptyList_WhenNoActuatorsAreFound()
    {
        //Arrange
        var noOfFoundActuators = 0;
        var uid = _fixture.Create<string>();

        var actuators = _fixture.CreateMany<Actuator>(noOfFoundActuators).ToList();

        var dto = GetActuatorFromPCBADto.From(actuators);
        _bus.Send(Arg.Any<GetActuatorFromPCBAQuery>(), Arg.Any<CancellationToken>()).Returns(dto);
        
        //Act
        var result = await _controller.GetAsync(uid, null, CancellationToken.None) as ObjectResult;
        
        //Assert
        Assert.IsType<OkObjectResult>(result);
        
        Assert.NotNull(result.Value);
        Assert.IsAssignableFrom<IEnumerable<GetActuatorFromPCBAActuator>>(result.Value);
        IEnumerable<GetActuatorFromPCBAActuator> resultAsEnumerable = (IEnumerable<GetActuatorFromPCBAActuator>)result.Value;
        Assert.Empty(resultAsEnumerable);
    }
    
}

