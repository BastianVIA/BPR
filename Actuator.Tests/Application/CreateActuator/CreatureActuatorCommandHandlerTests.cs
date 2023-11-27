using Application.CreateActuator;
using AutoFixture;
using Domain.Repositories;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Actuator.Tests.Application.CreateActuator;

public class CreatureActuatorCommandHandlerTests
{
    private Fixture _fixture = new();
    private IActuatorRepository _repository = Substitute.For<IActuatorRepository>();
    private CreateActuatorCommandHandler _commandHandler;
    
    public CreatureActuatorCommandHandlerTests()
    {
        _commandHandler = new CreateActuatorCommandHandler(_repository);
    }

    [Fact]
    public async Task Handle_ShouldPassAnActuatorToRepository_WhenCalled()
    {
        var request = _fixture.Create<CreateActuatorCommand>();

        await _commandHandler.Handle(request, CancellationToken.None);

        await _repository.Received(1).CreateActuator(Arg.Any<Domain.Entities.Actuator>());
    }

    [Fact]
    public async Task Handle_ThrowsException_OnError()
    {
        var request = _fixture.Create<CreateActuatorCommand>();

        _repository.CreateActuator(Arg.Any<Domain.Entities.Actuator>()).Throws<Exception>();

        await Assert.ThrowsAsync<Exception>(() => _commandHandler.Handle(request, CancellationToken.None));
    }
}