using AutoFixture;
using NSubstitute;
using TestResult.Application.GetAllTesters;
using TestResult.Domain.RepositoryInterfaces;

namespace TestResult.Tests.Application.GetAllTesters;

public class GetAllTestersQueryHandlerTests
{
    private Fixture _fixture = new();
    private GetAllTestersQueryHandler _handler;
    private ITestResultRepository _resultRepository = Substitute.For<ITestResultRepository>();
    
    public GetAllTestersQueryHandlerTests()
    {
        _handler = new GetAllTestersQueryHandler(_resultRepository);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoTestersExists()
    {
        //Arrange
        var request = _fixture.Create<GetAllTestersQuery>();
        
        _resultRepository.GetAllTesters().Returns(new List<string>());

        //Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.AllTesters);
        Assert.Empty(result.AllTesters);
    }
    
    [Fact]
    public async Task Handle_ReturnsSingleTester_WhenSingleTestersExists()
    {
        //Arrange
        var request = _fixture.Create<GetAllTestersQuery>();
        var tester = "Tester1";
        _resultRepository.GetAllTesters().Returns(new List<string>(){tester});

        //Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.AllTesters);
        Assert.Single(result.AllTesters);
        Assert.Equal(tester, result.AllTesters[0]);
    }
    
    [Fact]
    public async Task Handle_ReturnsManyTester_WhenManyTestersExists()
    {
        //Arrange
        var request = _fixture.Create<GetAllTestersQuery>();
        var tester = "Tester1";
        var tester2 = "Tester2";
        var tester3 = "Tester3";
        _resultRepository.GetAllTesters().Returns(new List<string>(){tester, tester2, tester3});

        //Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.AllTesters);
        Assert.Equal(3, result.AllTesters.Count);
        Assert.Equal(tester, result.AllTesters[0]);
        Assert.Equal(tester2, result.AllTesters[1]);
        Assert.Equal(tester3, result.AllTesters[2]);
    }
}