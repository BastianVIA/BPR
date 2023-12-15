using AutoFixture;
using BuildingBlocks.Application;
using NSubstitute;
using TestResult.Application.GetTestErrorsWithFilter;
using TestResult.Domain.Entities;
using TestResult.Domain.RepositoryInterfaces;
using TestResult.Tests.Util;

namespace TestResult.Tests.Application.GetTestErrorsWithFilter;

public class GetTestErrorsWithFilterQueryHandlerTests
{
    private Fixture _fixture = new();
    private IQueryBus _queryBus = Substitute.For<IQueryBus>();
    private ITestErrorRepository _errorRepository = Substitute.For<ITestErrorRepository>();
    private GetTestErrorsWithFilterQueryHandler _handler;

    public GetTestErrorsWithFilterQueryHandlerTests()
    {
        _handler = new GetTestErrorsWithFilterQueryHandler(_errorRepository, _queryBus);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyIntervals_WhenNoTestErrorsExists()
    {
        //Arrange
        var startTime = new DateTime(2023, 1, 1, 8, 0, 0);
        var endTime = new DateTime(2023, 1, 1, 10, 0, 0);
        var request = GetTestErrorsWithFilterQuery.Create(60, startTime, endTime);

        List<TestError> fromRepoList = new();

        _errorRepository.GetTestErrorsWithFilter(Arg.Any<int?>(), Arg.Any<string?>(), Arg.Any<int?>(), Arg.Any<int?>(),
            Arg.Any<DateTime?>(), Arg.Any<DateTime?>()).Returns(fromRepoList);
        //Act
        var result = await _handler.Handle(request, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.NotNull(result.PossibleErrorCodes);
        Assert.NotNull(result.DataLines);
        Assert.Equal(2, result.DataLines.Count);
        Assert.Equal(0, result.DataLines[0].TotalErrors);
        Assert.Equal(0, result.DataLines[1].TotalErrors);
    }
    
    [Fact]
    public async Task Handle_ReturnsIntervalsWithError_WhenSingleTestErrorsExists()
    {
        //Arrange
        var startTime = new DateTime(2023, 1, 1, 8, 0, 0);
        var endTime = new DateTime(2023, 1, 1, 10, 0, 0);
        var request = GetTestErrorsWithFilterQuery.Create(60, startTime, endTime);
        
        var error = EntityCreator.CreateTestError(timeOccured: startTime.AddMinutes(10));

        List<TestError> emptyList = new() { error };

        _errorRepository.GetTestErrorsWithFilter(Arg.Any<int?>(), Arg.Any<string?>(), Arg.Any<int?>(), Arg.Any<int?>(),
            Arg.Any<DateTime?>(), Arg.Any<DateTime?>()).Returns(emptyList);
        //Act
        var result = await _handler.Handle(request, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.NotNull(result.PossibleErrorCodes);
        Assert.NotNull(result.DataLines);
        Assert.Equal(2, result.DataLines.Count);
        Assert.Equal(1, result.DataLines[0].TotalErrors);
        Assert.Equal(0, result.DataLines[1].TotalErrors);
    }
    
    [Fact]
    public async Task Handle_ReturnsIntervalsWithMultipleErrors_WhenTestErrorsExists()
    {
        //Arrange
        var startTime = new DateTime(2023, 1, 1, 8, 0, 0);
        var endTime = new DateTime(2023, 1, 1, 10, 0, 0);
        var request = GetTestErrorsWithFilterQuery.Create(60, startTime, endTime);
        
        var error = EntityCreator.CreateTestError(timeOccured: startTime.AddMinutes(10));
        var error2 = EntityCreator.CreateTestError(timeOccured: startTime.AddMinutes(20));
        var error3 = EntityCreator.CreateTestError(timeOccured: startTime.AddMinutes(70));
        var error4 = EntityCreator.CreateTestError(timeOccured: startTime.AddMinutes(80));

        List<TestError> emptyList = new() { error, error2, error3,error4 };

        _errorRepository.GetTestErrorsWithFilter(Arg.Any<int?>(), Arg.Any<string?>(), Arg.Any<int?>(), Arg.Any<int?>(),
            Arg.Any<DateTime?>(), Arg.Any<DateTime?>()).Returns(emptyList);
        //Act
        var result = await _handler.Handle(request, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.NotNull(result.PossibleErrorCodes);
        Assert.NotNull(result.DataLines);
        Assert.Equal(2, result.DataLines.Count);
        Assert.Equal(2, result.DataLines[0].TotalErrors);
        Assert.Equal(2, result.DataLines[1].TotalErrors);
    }
    
    [Fact]
    public async Task Handle_ReturnsIntervalsWithCorrectErrorCodes_WhenTestErrorExists()
    {
        //Arrange
        var startTime = new DateTime(2023, 1, 1, 8, 0, 0);
        var endTime = new DateTime(2023, 1, 1, 10, 0, 0);
        var request = GetTestErrorsWithFilterQuery.Create(60, startTime, endTime);
        var errorCode = 70;
        var error = EntityCreator.CreateTestError(timeOccured: startTime.AddMinutes(10), errorCode: errorCode);

        List<TestError> emptyList = new() { error};

        _errorRepository.GetTestErrorsWithFilter(Arg.Any<int?>(), Arg.Any<string?>(), Arg.Any<int?>(), Arg.Any<int?>(),
            Arg.Any<DateTime?>(), Arg.Any<DateTime?>()).Returns(emptyList);
        //Act
        var result = await _handler.Handle(request, CancellationToken.None);
        
        Assert.NotNull(result);
        Assert.NotNull(result.PossibleErrorCodes);
        Assert.NotNull(result.DataLines);
        Assert.Equal(2, result.DataLines.Count);
        Assert.Equal(1, result.DataLines[0].TotalErrors);
        Assert.Equal(0, result.DataLines[1].TotalErrors);
        
        Assert.Equal(errorCode, result.PossibleErrorCodes[0].ErrorCode);
        Assert.Equal(errorCode, result.DataLines[0].ListOfErrors[0].ErrorCode);

    }
}