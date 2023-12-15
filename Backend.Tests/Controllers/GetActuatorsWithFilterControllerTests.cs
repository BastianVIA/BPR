using Application.GetActuatorsWithFilter;
using AutoFixture;
using Backend.Controllers.Actuator;
using Backend.Tests.Util;
using BuildingBlocks.Application;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Backend.Tests.Controllers;

public class GetActuatorsWithFilterControllerTests
{
    private Fixture _fixture = new();
    private GetActuatorsWithFilterController _controller;
    private readonly IQueryBus _bus = Substitute.For<IQueryBus>();
    
    public GetActuatorsWithFilterControllerTests()
    {
        _controller = new GetActuatorsWithFilterController(_bus);
    }

    [Fact]
    public async Task GetAsync_ReturnsManyActuators_WhenWOHasManyMatches()
    {
        // Arrange
        var noOfActuators = 3;
        var wo = _fixture.Create<int>();
        var request = _fixture.Build<GetActuatorsWithFilterQuery>()
            .With(q => q.WorkOrderNumber, wo)
            .Create();
        var expectedList = new List<Actuator>();
        expectedList.AddMany(() => EntityCreator.CreateActuator(woNo: wo), noOfActuators);
        
        var expected = GetActuatorsWithFilterDto.From(expectedList);
        
        _bus.Send(Arg.Any<GetActuatorsWithFilterQuery>(), CancellationToken.None)
            .Returns(expected);
        
        // Act
        var result = await _controller.GetAsync(request, CancellationToken.None) as ObjectResult;
        
        // Assert
        Assert.NotNull(result);
        var response = result.Value;
        
        Assert.IsType<GetActuatorsWithFilterController.GetActuatorWithFilterResponse>(response);
        var casted = (GetActuatorsWithFilterController.GetActuatorWithFilterResponse)response;
        Assert.Equal(noOfActuators, casted.Actuators.Count);
        foreach (var actuator in casted.Actuators)
        {
            Assert.Equal(wo, actuator.WorkOrderNumber);
        }
    }
}