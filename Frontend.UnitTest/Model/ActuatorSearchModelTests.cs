using Frontend.Model;
using Frontend.Networking;
using Frontend.Service;

namespace Frontend.UnitTest.Model;

public class ActuatorSearchModelTests
{
    private Fixture _fixture = new();
    private INetwork _network = Substitute.For<INetwork>();
    private IActuatorSearchModel _model;

    public ActuatorSearchModelTests()
    {
        _model = new ActuatorSearchModel(_network);
    }

    [Fact]
    public async Task GetActuatorsWithFilter_ReturnsEmptyList_WhenNoMatchesFound()
    {
        // Arrange
        var nonExistingUid = "THIsUiDDoesnOtEixst12345";
        var expectedResponse = _fixture.Build<GetActuatorWithFilterResponse>()
            .With(a => a.Actuators, new List<GetActuatorWithFilterActuator>())
            .Create();

        _network.GetActuatorWithFilter(
                Arg.Any<int?>(), 
                Arg.Any<int?>(), 
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<int?>(),
                Arg.Any<int?>(),
                Arg.Any<DateTime?>(),
                Arg.Any<DateTime?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>())
            .Returns(expectedResponse);
        
        // Act
        var result = await _model.GetActuatorWithFilter(null,
            null, 
            nonExistingUid,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null);
        
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetActuatorsWithFilter_ReturnsListOfOneActuator_WhenOneMatchExists()
    {
        // Arrange
        var noOfActuators = 1;
        var expectedUid = _fixture.Create<string>();
        var expectedResponse = _fixture.Build<GetActuatorWithFilterResponse>()
            .With(a => a.Actuators, 
                _fixture.Build<GetActuatorWithFilterActuator>()
                    .With(a => a.Pcba, 
                        _fixture.Build<GetActuatorWithFilterPCBA>()
                            .With(p => p.Uid, expectedUid)
                            .Create())
                    .CreateMany(noOfActuators)
                    .ToList())
            .Create();

        _network.GetActuatorWithFilter(Arg.Any<int?>(), 
                Arg.Any<int?>(), 
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<int?>(),
                Arg.Any<int?>(),
                Arg.Any<DateTime?>(),
                Arg.Any<DateTime?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>())
            .Returns(expectedResponse);
        
        // Act
        var result = await _model.GetActuatorWithFilter(null,
            null, 
            expectedUid,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null);
        
        // Assert
        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Equal(expectedResponse.Actuators.First().Pcba.Uid, result[0].PCBA.PCBAUid);

        await _network.Received(1).GetActuatorWithFilter(Arg.Any<int?>(), 
            Arg.Any<int?>(), 
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<int?>(),
            Arg.Any<int?>(),
            Arg.Any<DateTime?>(),
            Arg.Any<DateTime?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<string?>());
    }

    [Fact]
    public async Task GetActuatorsByPCBA_ReturnsMultipleActuators_WhenUidMatches()
    {
        // Arrange
        var noOfActuators = 3;
        var expectedUid = _fixture.Create<string>();
        var expectedResponse = _fixture.Build<GetActuatorWithFilterResponse>()
            .With(a => a.Actuators, _fixture.Build<GetActuatorWithFilterActuator>()
                .With(a => a.Pcba, _fixture.Build<GetActuatorWithFilterPCBA>()
                    .With(p => p.Uid, expectedUid)
                    .Create())
                .CreateMany(noOfActuators)
                .ToList())
            .Create();
            

        _network.GetActuatorWithFilter(Arg.Any<int?>(), 
                Arg.Any<int?>(), 
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<int?>(),
                Arg.Any<int?>(),
                Arg.Any<DateTime?>(),
                Arg.Any<DateTime?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>(),
                Arg.Any<string?>())
            .Returns(expectedResponse);
        
        // Act
        var result = await _model.GetActuatorWithFilter(null,
            null, 
            expectedUid,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(noOfActuators, result.Count);

        foreach (var actuator in result)
        {
            Assert.Equal(expectedUid, actuator.PCBA.PCBAUid);
        }
    }
}