using AutoFixture;
using BuildingBlocks.Infrastructure.Database.Transaction;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TestResult.Application.CreateTestError;
using TestResult.Domain.Entities;
using TestResult.Domain.RepositoryInterfaces;

namespace TestResult.Tests.Application.CreateTestError;

public class CreateTestErrorCommandHandlerTests
{
    private Fixture _fixture = new();
    private CreateTestErrorCommandHandler _handler;
    private IDbTransaction _dbTransaction = Substitute.For<IDbTransaction>();
    private ITestErrorRepository _errorRepository = Substitute.For<ITestErrorRepository>();
    
    public CreateTestErrorCommandHandlerTests()
    {
        _handler = new CreateTestErrorCommandHandler(_errorRepository, _dbTransaction);
    }

    [Fact]
    public async Task Handle_ShouldPassATestResultToRepository_WhenCalled()
    {
        //Arrange
        var request = _fixture.Create<CreateTestErrorCommand>();
        
        //Act
        await _handler.Handle(request, CancellationToken.None);
        
        //Assert
        await _errorRepository.Received(1).CreateTestError(Arg.Any<TestError>());
    }
    
    [Fact]
    public async Task Handle_ThrowsException_OnError()
    {
        //Arrange
        var request = _fixture.Create<CreateTestErrorCommand>();

        //Act
        _errorRepository.CreateTestError(Arg.Any<TestError>()).Throws<Exception>();

        //Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
    }
}