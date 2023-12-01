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
    public async Task GetActuatorsByPCBA_ReturnsEmptyList_WhenNoMatchesFound()
    {
        // Arrange
        var nonExistingUid = "THIsUiDDoesnOtEixst12345";
        var expectedResponse = _fixture.Build<GetActuatorFromPCBAResponse>()
            .With(a => a.Actuators, new List<GetActuatorFromPCBAActuator>())
            .Create();

        _network.GetActuatorFromPCBA(Arg.Any<string>(), Arg.Any<int?>())
            .Returns(expectedResponse);
        
        // Act
        var result = await _model.GetActuatorsByPCBA(nonExistingUid);
        
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetActuatorsByPCBA_ReturnsListOfOneActuator_WhenOneMatchExists()
    {
        // Arrange
        var noOfActuators = 1;
        var expectedUid = _fixture.Create<string>();
        var expectedResponse = _fixture.Build<GetActuatorFromPCBAResponse>()
            .With(a => a.Actuators, _fixture.Build<GetActuatorFromPCBAActuator>()
                .With(actuator => actuator.Uid, expectedUid)
                .CreateMany(noOfActuators)
                .ToList())
            .Create();

        _network.GetActuatorFromPCBA(Arg.Any<string>(), Arg.Any<int?>())
            .Returns(expectedResponse);
        
        // Act
        var result = await _model.GetActuatorsByPCBA(expectedUid);
        
        // Assert
        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Equal(expectedResponse.Actuators.First().Uid, result[0].PCBA.PCBAUid);

        await _network.Received(1).GetActuatorFromPCBA(expectedUid, Arg.Any<int?>());
    }

    [Fact]
    public async Task GetActuatorsByPCBA_ReturnsMultipleActuators_WhenUidMatches()
    {
        // Arrange
        var noOfActuators = 3;
        var expectedUid = _fixture.Create<string>();
        var expectedReponse = _fixture.Build<GetActuatorFromPCBAResponse>()
            .With(a => a.Actuators, _fixture.Build<GetActuatorFromPCBAActuator>()
                .With(actuator => actuator.Uid, expectedUid)
                .CreateMany(noOfActuators)
                .ToList())
            .Create();
            

        _network.GetActuatorFromPCBA(Arg.Any<string>(), Arg.Any<int?>())
            .Returns(expectedReponse);
        
        // Act
        var result = await _model.GetActuatorsByPCBA(expectedUid);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(noOfActuators, result.Count);

        foreach (var actuator in result)
        {
            Assert.Equal(expectedUid, actuator.PCBA.PCBAUid);
        }
    }
}