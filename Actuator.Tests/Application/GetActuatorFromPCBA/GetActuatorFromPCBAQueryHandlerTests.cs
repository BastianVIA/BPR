using System.Net.NetworkInformation;
using Application.GetActuatorFromPCBA;
using AutoFixture;
using Domain.RepositoryInterfaces;
using NSubstitute;

namespace Actuator.Tests.Application.GetActuatorFromPCBA;

public class GetActuatorFromPCBAQueryHandlerTests
{
    private Fixture _fixture = new();
    private IActuatorRepository _repository = Substitute.For<IActuatorRepository>();
    private GetActuatorFromPCBAQueryHandler _handler;

    public GetActuatorFromPCBAQueryHandlerTests()
    {
        _handler = new GetActuatorFromPCBAQueryHandler(_repository);
    }

    [Fact]
    public async Task Handle_ReturnsActuatorDtos_WhenActuatorsExists()
    {
        //Arrange
        var request = _fixture.Create<GetActuatorFromPCBAQuery>();
        var noOfActuatorsToReturn = 3;
        var actuators = _fixture.CreateMany<Domain.Entities.Actuator>(noOfActuatorsToReturn).ToList();

        _repository.GetActuatorsFromPCBAAsync(Arg.Any<string>(), Arg.Any<int?>()).Returns(actuators);
        //Act
        var result = await _handler.Handle(request, CancellationToken.None);
        
        //Assert
        Assert.NotEmpty(result.Actuators);
        Assert.Equal(noOfActuatorsToReturn, result.Actuators.Count);
        AssertActuatorEqual(actuators[0], result.Actuators[0]);
        AssertActuatorEqual(actuators[1], result.Actuators[1]);
        AssertActuatorEqual(actuators[2], result.Actuators[2]);
        
    }

    [Fact]
    public async Task Handle_ReturnsDtoWithEmptyList_WhenNoActuatorsFound()
    {
        //Arrange
        var request = _fixture.Create<GetActuatorFromPCBAQuery>();
        var noOfActuatorsToReturn = 0;
        var actuators = _fixture.CreateMany<Domain.Entities.Actuator>(noOfActuatorsToReturn).ToList();

        _repository.GetActuatorsFromPCBAAsync(Arg.Any<string>(), Arg.Any<int?>()).Returns(actuators);
        //Act
        var result = await _handler.Handle(request, CancellationToken.None);
        
        //Assert
        Assert.Empty(result.Actuators);
        Assert.Equal(noOfActuatorsToReturn, result.Actuators.Count);
    }

    private void AssertActuatorEqual(Domain.Entities.Actuator expected, GetActuatorFromPCBAActuatordto actual)
    {
        Assert.Equal(expected.Id.WorkOrderNumber, actual.WorkOrderNumber);
        Assert.Equal(expected.Id.SerialNumber, actual.SerialNumber);
        Assert.Equal(expected.PCBA.Uid, actual.Uid);
        Assert.Equal(expected.PCBA.ManufacturerNumber, actual.ManufacturerNumber);
    }
}