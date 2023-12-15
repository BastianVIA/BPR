using Application.CreateActuator;
using AutoFixture;
using BuildingBlocks.Infrastructure.Database.Transaction;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Actuator.Tests.Application.CreateActuator;

public class CreatureActuatorCommandHandlerTests
{
    private Fixture _fixture = new();
    private IActuatorRepository _actuatorRepository = Substitute.For<IActuatorRepository>();
    private IPCBARepository _pcbaRepository = Substitute.For<IPCBARepository>();
    private IDbTransaction _dbTransaction = Substitute.For<IDbTransaction>();
    private CreateActuatorCommandHandler _commandHandler;
    
    public CreatureActuatorCommandHandlerTests()
    {
        _commandHandler = new CreateActuatorCommandHandler(_actuatorRepository, _pcbaRepository, _dbTransaction);
    }

    [Fact]
    public async Task Handle_ShouldPassAnActuatorToRepository_WhenCalled()
    {
        // Arrange
        var pcba = _fixture.Create<PCBA>();
        var request = _fixture.Create<CreateActuatorCommand>();

        _pcbaRepository.GetPCBA(Arg.Any<string>())
            .Returns(pcba);
        // Act
        await _commandHandler.Handle(request, CancellationToken.None);

        await _actuatorRepository.Received(1).CreateActuator(Arg.Any<Domain.Entities.Actuator>());
    }

    [Fact]
    public async Task Handle_ThrowsException_OnError()
    {
        var request = _fixture.Create<CreateActuatorCommand>();

        _actuatorRepository.CreateActuator(Arg.Any<Domain.Entities.Actuator>()).Throws<NullReferenceException>();

        await Assert.ThrowsAsync<NullReferenceException>(() => _commandHandler.Handle(request, CancellationToken.None));
    }
}