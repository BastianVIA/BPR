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
        var nonExistingUid = "THIsUiDDoesnOtEixst12345";
        var expectedList = new List<GetActuatorFromPCBAActuator>();

        _network.GetActuatorFromPCBA(Arg.Any<string>(), Arg.Any<int?>())
            .Returns(expectedList);

        var result = await _model.GetActuatorsByPCBA(nonExistingUid);
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetActuatorsByPCBA_ReturnsListOfOneActuator_WhenOneMatchExists()
    {
        var noOfActuators = 1;
        var expectedUid = _fixture.Create<string>();
        var expectedList = _fixture.Build<GetActuatorFromPCBAActuator>()
            .With(a => a.Uid, expectedUid)
            .CreateMany(noOfActuators)
            .ToList();

        _network.GetActuatorFromPCBA(Arg.Any<string>(), Arg.Any<int?>())
            .Returns(expectedList);

        var result = await _model.GetActuatorsByPCBA(expectedUid);
        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Equal(expectedList[0].Uid, result[0].PCBA.PCBAUid);

        await _network.Received(1).GetActuatorFromPCBA(expectedUid, Arg.Any<int?>());
    }

    [Fact]
    public async Task GetActuatorsByPCBA_ReturnsMultipleActuators_WhenUidMatches()
    {
        var noOfActuators = 3;
        var expectedUid = _fixture.Create<string>();
        var expectedList = _fixture.Build<GetActuatorFromPCBAActuator>()
            .With(a => a.Uid, expectedUid)
            .CreateMany(noOfActuators)
            .ToList();

        _network.GetActuatorFromPCBA(Arg.Any<string>(), Arg.Any<int?>())
            .Returns(expectedList);

        var result = await _model.GetActuatorsByPCBA(expectedUid);
        Assert.NotNull(result);
        Assert.Equal(noOfActuators, result.Count);

        foreach (var actuator in result)
        {
            Assert.Equal(expectedUid, actuator.PCBA.PCBAUid);
        }
    }
}