using AutoFixture;
using Backend.Controllers.TestResult;
using BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using TestResult.Application.GetTestResultsWithFilter;

namespace Backend.Tests.Controllers.TestResult;

public class GetTestResultsWithFilterControllerTests
{
    private Fixture _fixture = new();
    private GetTestResultsWithFilterController _controller;
    private readonly IQueryBus _bus = Substitute.For<IQueryBus>();

    public GetTestResultsWithFilterControllerTests()
    {
        _controller = new GetTestResultsWithFilterController(_bus);
    }
    
    [Fact]
    public async Task GetAsync_ReturnsOk_WhenNoTesterExists()
    {
        //Arrange
        List<global::TestResult.Domain.Entities.TestResult> emptyResults = new();

        var request = _fixture.Create<GetTestResultsWithFilterQuery>();
        var dto = GetTestResultsWithFilterDto.From(emptyResults);

        _bus.Send(Arg.Any<GetTestResultsWithFilterQuery>(), CancellationToken.None).Returns(dto);
        
        //Act
        var result = await _controller.GetAsync(request, CancellationToken.None);
        
        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

}