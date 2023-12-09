using Application.CreatePCBA;
using AutoFixture;
using Domain.RepositoryInterfaces;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Actuator.Tests.Application.CreatePCBA;

public class CreatePCBACommandHandlerTests
{
    private Fixture _fixture = new();
    private IPCBARepository _pcbaRepository = Substitute.For<IPCBARepository>();
    private CreateOrUpdatePCBACommandHandler _commandHandler;

    public CreatePCBACommandHandlerTests()
    {
        _commandHandler = new CreateOrUpdatePCBACommandHandler(_pcbaRepository);
    }

    [Fact]
    public async Task Handle_ShouldPassCreatePCBAToRepository_WhenCalled()
    {
        var request = _fixture.Create<CreateOrUpdatePCBACommand>();
        
        _pcbaRepository.UpdatePCBA(Arg.Any<Domain.Entities.PCBA>()).Throws<KeyNotFoundException>();

        await _commandHandler.Handle(request, CancellationToken.None);

        await _pcbaRepository.Received(1).CreatePCBA(Arg.Any<Domain.Entities.PCBA>());
    }

    [Fact]
    public async Task Handle_ThrowsException_OnError()
    {
        var request = _fixture.Create<CreateOrUpdatePCBACommand>();
        
        _pcbaRepository.UpdatePCBA(Arg.Any<Domain.Entities.PCBA>()).Throws<Exception>();

        await Assert.ThrowsAsync<Exception>(() => _commandHandler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldPassUpdatePCBAToRepository_WhenCalled()
    {
        var request = _fixture.Create<CreateOrUpdatePCBACommand>();

        await _commandHandler.Handle(request, CancellationToken.None);

        await _pcbaRepository.Received(1).UpdatePCBA(Arg.Any<Domain.Entities.PCBA>());
    }
}