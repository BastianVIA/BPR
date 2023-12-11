using Application.CreateActuator;
using AutoFixture;
using BuildingBlocks.Infrastructure.Database.Transaction;
using Domain.RepositoryInterfaces;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Actuator.Tests.Application.CreateActuator;

public class CreatureActuatorCommandHandlerTests
{
    private Fixture _fixture = new();
    private IActuatorRepository _actuatorRepository = Substitute.For<IActuatorRepository>();
    private IPCBARepository _pcbaRepository = Substitute.For<IPCBARepository>();
    private CreateActuatorCommandHandler _commandHandler;
    private IDbTransaction _dbTransaction = Substitute.For<IDbTransaction>();
    
    public CreatureActuatorCommandHandlerTests()
    {
        _commandHandler = new CreateActuatorCommandHandler(_actuatorRepository, _pcbaRepository, _dbTransaction);
    }

    [Fact]
    public async Task Handle_ShouldPassAnActuatorToRepository_WhenCalled()
    {
        var request = _fixture.Create<CreateActuatorCommand>();

        await _commandHandler.Handle(request, CancellationToken.None);

        await _actuatorRepository.Received(1).CreateActuator(Arg.Any<Domain.Entities.Actuator>());
    }

    [Fact]
    public async Task Handle_ThrowsException_OnError()
    {
        var request = _fixture.Create<CreateActuatorCommand>();

        _actuatorRepository.CreateActuator(Arg.Any<Domain.Entities.Actuator>()).Throws<Exception>();

        await Assert.ThrowsAsync<Exception>(() => _commandHandler.Handle(request, CancellationToken.None));
    }
}