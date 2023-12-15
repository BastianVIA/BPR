using AutoFixture;
using BuildingBlocks.Infrastructure.Database.Transaction;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TestResult.Application.CreateTestResult;
using TestResult.Domain.RepositoryInterfaces;

namespace TestResult.Tests.Application.CreateTestResult;

public class CreateTestResultCommandHandlerTests
{
    private Fixture _fixture = new();
    private CreateTestResultCommandHandler _handler;
    private IDbTransaction _dbTransaction = Substitute.For<IDbTransaction>();
    private ITestResultRepository _resultRepository = Substitute.For<ITestResultRepository>();

    public CreateTestResultCommandHandlerTests()
    {
        _handler = new CreateTestResultCommandHandler(_resultRepository, _dbTransaction);
    }

    [Fact]
    public async Task Handle_ShouldPassATestResultrToRepository_WhenCalled()
    {
        //Arrange
        var request = _fixture.Create<CreateTestResultCommand>();
        
        //Act
        await _handler.Handle(request, CancellationToken.None);

        //Assert
        await _resultRepository.Received(1).CreateTestResult(Arg.Any<Domain.Entities.TestResult>());
    }
    
    [Fact]
    public async Task Handle_ThrowsException_OnError()
    {
        //Arrange
        var request = _fixture.Create<CreateTestResultCommand>();

        //Act
        _resultRepository.CreateTestResult(Arg.Any<Domain.Entities.TestResult>()).Throws<Exception>();

        //Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
    }
}