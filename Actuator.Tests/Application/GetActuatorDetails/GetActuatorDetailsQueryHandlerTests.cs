using Application.GetActuatorDetails;
using AutoFixture;
using Domain.Entities;
using Domain.Repositories;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Actuator.Tests.Application.GetActuatorDetails;

public class GetActuatorDetailsQueryHandlerTests
{
    private Fixture _fixture = new();
    private IActuatorRepository _repository = Substitute.For<IActuatorRepository>();
    private GetActuatorDetailsQueryHandler _handler;

    public GetActuatorDetailsQueryHandlerTests()
    {
        _handler = new GetActuatorDetailsQueryHandler(_repository);
    }

    [Fact]
    public async Task Handle_ReturnsActuatorDto_WhenActuatorExists()
    {
        var request = _fixture.Create<GetActuatorDetailsQuery>();
        var actuator = _fixture.Create<Domain.Entities.Actuator>();

        _repository.GetActuator(Arg.Any<CompositeActuatorId>())
            .Returns(actuator);
        
        var result = await _handler.Handle(request, CancellationToken.None);
        Assert.NotNull(result);
        Assert.IsType<GetActuatorDetailsDto>(result);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenActuatorIsNotFound()
    {
        var request = _fixture.Create<GetActuatorDetailsQuery>();

        _repository.GetActuator(Arg.Any<CompositeActuatorId>())
            .ThrowsAsync<Exception>();
        
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
    }
}