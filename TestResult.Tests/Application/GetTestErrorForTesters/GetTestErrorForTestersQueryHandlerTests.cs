using AutoFixture;
using NSubstitute;
using TestResult.Application.GetTestErrorForTesters;
using TestResult.Domain.Entities;
using TestResult.Domain.RepositoryInterfaces;
using TestResult.Tests.Util;

namespace TestResult.Tests.Application.GetTestErrorForTesters;

public class GetTestErrorForTestersQueryHandlerTests
{
    private Fixture _fixture = new();
    private ITestErrorRepository _errorRepository = Substitute.For<ITestErrorRepository>();
    private GetTestErrorForTestersQueryHandler _handler;

    public GetTestErrorForTestersQueryHandlerTests()
    {
        _handler = new GetTestErrorForTestersQueryHandler(_errorRepository);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyErrorsForTesters_WhenNoTestersProvided()
    {
        //Arrange
        var testers = new List<string>() { };
        var request = GetTestErrorForTestersQuery.Create(testers, TesterTimePeriodEnum.This_Week);

        List<TestError> fromRepoList = new();

        _errorRepository.GetAllErrorsForTesterSince(Arg.Any<List<string>>(),
            Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(fromRepoList);
        //Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ErrorsForTesters);
        Assert.Empty(result.ErrorsForTesters);
    }


    [Fact]
    public async Task Handle_ReturnsListWithEmptyErrorsForTesters_WhenNoErrorsExists()
    {
        //Arrange
        var testers = new List<string>() { "Tester1" };
        var request = GetTestErrorForTestersQuery.Create(testers, TesterTimePeriodEnum.This_Week);

        List<TestError> fromRepoList = new();

        _errorRepository.GetAllErrorsForTesterSince(Arg.Any<List<string>>(),
            Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(fromRepoList);
        //Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ErrorsForTesters);
        Assert.NotNull(result.ErrorsForTesters[0].Errors);
    }

    [Fact]
    public async Task Handle_ReturnsListWithErrorsForTesters_WhenSingleErrorExists()
    {
        //Arrange
        var tester = "Tester1";
        var testers = new List<string>() { tester };
        var request = GetTestErrorForTestersQuery.Create(testers, TesterTimePeriodEnum.This_Year);

        var error = EntityCreator.CreateTestError(timeOccured: DateTime.Now.AddHours(-5), tester: tester);


        List<TestError> fromRepoList = new() { error };

        _errorRepository.GetAllErrorsForTesterSince(Arg.Any<List<string>>(),
            Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(fromRepoList);
        //Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ErrorsForTesters);
        Assert.NotEmpty(result.ErrorsForTesters[0].Errors);

        var errorCount = result.ErrorsForTesters[0].Errors.Where(e => e.ErrorCount != 0).ToList().Count;
        Assert.Equal(1, errorCount);
    }

    [Fact]
    public async Task Handle_ReturnsListWithErrorsForTesters_WhenSingleErrorExistsForMultipleTesters()
    {
        //Arrange
        var tester = "Tester1";
        var tester2 = "Tester2";
        var testers = new List<string>() { tester, tester2 };
        var request = GetTestErrorForTestersQuery.Create(testers, TesterTimePeriodEnum.This_Year);

        var error1 = EntityCreator.CreateTestError(timeOccured: DateTime.Now.AddHours(-3), tester: tester);
        var error2 = EntityCreator.CreateTestError(timeOccured: DateTime.Now.AddHours(-5), tester: tester2);


        List<TestError> fromRepoList = new() { error1, error2 };

        _errorRepository.GetAllErrorsForTesterSince(Arg.Any<List<string>>(),
            Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(fromRepoList);
        //Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ErrorsForTesters);
        Assert.Equal(2, result.ErrorsForTesters.Count);

        var errorCount = result.ErrorsForTesters[0].Errors.Where(e => e.ErrorCount != 0).ToList().Count;
        Assert.Equal(1, errorCount);
        var errorCount2 = result.ErrorsForTesters[1].Errors.Where(e => e.ErrorCount != 0).ToList().Count;
        Assert.Equal(1, errorCount2);
    }


    [Fact]
    public async Task Handle_ReturnsListWithErrorsForTesters_WhenMultipleErrorExists()
    {
        //Arrange
        var tester = "Tester1";
        var testers = new List<string>() { tester };
        var request = GetTestErrorForTestersQuery.Create(testers, TesterTimePeriodEnum.This_Year);

        var error = EntityCreator.CreateTestError(timeOccured: DateTime.Now.AddHours(-5), tester: tester);
        var error2 = EntityCreator.CreateTestError(timeOccured: DateTime.Now.AddHours(-4), tester: tester);
        var error3 = EntityCreator.CreateTestError(timeOccured: DateTime.Now.AddHours(-3), tester: tester);


        List<TestError> fromRepoList = new() { error, error2, error3 };

        _errorRepository.GetAllErrorsForTesterSince(Arg.Any<List<string>>(),
            Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(fromRepoList);
        //Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ErrorsForTesters);
        Assert.NotEmpty(result.ErrorsForTesters[0].Errors);

        var errorDtos = result.ErrorsForTesters[0].Errors.Where(e => e.ErrorCount != 0).ToList();
        Assert.Single(errorDtos);
        Assert.Equal(3, errorDtos[0].ErrorCount);
    }
}