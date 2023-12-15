using AutoFixture;
using NSubstitute;
using TestResult.Application.GetTestResultsWithFilter;
using TestResult.Domain.RepositoryInterfaces;


namespace TestResult.Tests.Application.GetTestResultsWithFilter;

public class GetTestResultsWithFilterQueryHandlerTests
{
    private Fixture _fixture = new();
    private GetTestResultsWithFilterQueryHandler _handler;
    private ITestResultRepository _resultRepository = Substitute.For<ITestResultRepository>();

    public GetTestResultsWithFilterQueryHandlerTests()
    {
        _handler = new GetTestResultsWithFilterQueryHandler(_resultRepository);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoResultsMatchFilter()
    {
        //Arrange
        var request = _fixture.Create<GetTestResultsWithFilterQuery>();
        List<Domain.Entities.TestResult> fromRepoList = new();

        _resultRepository.GetActuatorsTestWithFilter(Arg.Any<int?>(), Arg.Any<int?>(), Arg.Any<string?>(),
            Arg.Any<int?>(), Arg.Any<DateTime?>(), Arg.Any<DateTime?>()).Returns(fromRepoList);

        //Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.TestResultDtos);
        Assert.Empty(result.TestResultDtos);
    }

    [Fact]
    public async Task Handle_ReturnsListWithSingleElement_WhenOneResultsMatchesFilter()
    {
        //Arrange
        var request = _fixture.Create<GetTestResultsWithFilterQuery>();
        var testResult = _fixture.Create<Domain.Entities.TestResult>();
        List<Domain.Entities.TestResult> fromRepoList = new() { testResult };

        _resultRepository.GetActuatorsTestWithFilter(Arg.Any<int?>(), Arg.Any<int?>(), Arg.Any<string?>(),
            Arg.Any<int?>(), Arg.Any<DateTime?>(), Arg.Any<DateTime?>()).Returns(fromRepoList);

        //Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.TestResultDtos);
        Assert.Single(result.TestResultDtos);
        testResultEqueal(testResult, result.TestResultDtos[0]);
    }


    [Fact]
    public async Task Handle_ReturnsListWithManyElements_WhenManyResultsMatchesFilter()
    {
        //Arrange
        var request = _fixture.Create<GetTestResultsWithFilterQuery>();
        var testResult = _fixture.Create<Domain.Entities.TestResult>();
        var testResult2 = _fixture.Create<Domain.Entities.TestResult>();
        var testResult3 = _fixture.Create<Domain.Entities.TestResult>();
        List<Domain.Entities.TestResult> fromRepoList = new() { testResult, testResult2, testResult3 };

        _resultRepository.GetActuatorsTestWithFilter(Arg.Any<int?>(), Arg.Any<int?>(), Arg.Any<string?>(),
            Arg.Any<int?>(), Arg.Any<DateTime?>(), Arg.Any<DateTime?>()).Returns(fromRepoList);

        //Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.TestResultDtos);
        Assert.Equal(3, result.TestResultDtos.Count);


        testResultEqueal(testResult, result.TestResultDtos[0]);
        testResultEqueal(testResult2, result.TestResultDtos[1]);
        testResultEqueal(testResult3, result.TestResultDtos[2]);
    }

    private void testResultEqueal(Domain.Entities.TestResult expected, TestResultsWithFilterDTO actual)
    {
        Assert.Equal(expected.WorkOrderNumber, actual.WorkOrderNumber);
        Assert.Equal(expected.SerialNumber, actual.SerialNumber);
        Assert.Equal(expected.Bay, actual.Bay);
        Assert.Equal(expected.MinServoPosition, actual.MinServoPosition);

        Assert.Equal(expected.MaxServoPosition, actual.MaxServoPosition);
        Assert.Equal(expected.MinBuslinkPosition, actual.MinBuslinkPosition);
        Assert.Equal(expected.MaxBuslinkPosition, actual.MaxBuslinkPosition);
        Assert.Equal(expected.TimeOccured, actual.TimeOccured);
        if (expected.TestErrors != null)
        {
            Assert.Equal(expected.TestErrors.Count, actual.TestErrors.Count);
        }
    }
}