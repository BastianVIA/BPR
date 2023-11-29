using Frontend.Entities;
using Frontend.Model;
using Frontend.Pages;

namespace Frontend.UnitTest.ViewModel;

public class ActuatorInfoTests
{
    private Fixture _fixture = new();
    private IActuatorSearchModel _model = Substitute.For<IActuatorSearchModel>();
    private ActuatorInfoBase _viewModel;

    public ActuatorInfoTests()
    {
        _viewModel = new ActuatorInfoBase(_model);
    }

    [Fact]
    public async Task SearchActuators_ShouldReturnEmptyList_WhenNoMatchesFound()
    {
        var expected = new List<Actuator>();

        _model.GetActuatorsByUid(Arg.Any<string>())
            .Returns(expected);
        
        await _viewModel.SearchActuators();
        
        Assert.True(_viewModel.actuators.Count == 0);
    }

    [Fact]
    public async Task SearchActuators_ShouldReturnOneActuator_WhenOneMatchFound()
    {
        var expected = new List<Actuator>
        {
            _fixture.Create<Actuator>()
        };

        _model.GetActuatorsByUid(Arg.Any<string>())
            .Returns(expected);
        
        await _viewModel.SearchActuators();
        
        Assert.NotEmpty(_viewModel.actuators);
        Assert.True(_viewModel.actuators.Count == 1);
    }

    [Fact]
    public async Task SearchActuators_ShouldReturnActuatorWithExactUid_WhenMatchFound()
    {
        var expectedUid = _fixture.Create<string>();
        _viewModel.SearchUid = expectedUid;
        var expectedList = new List<Actuator>
        {
            _fixture.Create<Actuator>().WithPCBAUid(expectedUid)
        };
        _model.GetActuatorsByUid(expectedUid)
            .Returns(expectedList);
        
        await _viewModel.SearchActuators();
        Assert.NotEmpty(_viewModel.actuators);
        Assert.Equal(expectedUid, _viewModel.actuators.First().PCBA.PCBAUid);
    }

    [Fact]
    public async Task SearchActuators_ShouldReturnManyActuators_WhenMultipleMatchesFound()
    {
        var expectedList = _fixture.CreateMany<Actuator>().ToList();

        _model.GetActuatorsByUid(Arg.Any<string>())
            .Returns(expectedList);

        await _viewModel.SearchActuators();
        
        Assert.NotEmpty(_viewModel.actuators);
        Assert.True(_viewModel.actuators.Count > 1);
    }

    [Fact]
    public async Task SearchActuators_ShouldReturnManyCorrectActuators_WhenMultipleMatchesFound()
    {
        var expectedUid = _fixture.Create<string>();
        _viewModel.SearchUid = expectedUid;

        var expectedList = new List<Actuator>
        {
            _fixture.Create<Actuator>().WithPCBAUid(expectedUid),
            _fixture.Create<Actuator>().WithPCBAUid(expectedUid),
            _fixture.Create<Actuator>().WithPCBAUid(expectedUid)
        };
        
        _model.GetActuatorsByUid(expectedUid)
            .Returns(expectedList);

        await _viewModel.SearchActuators();
        
        Assert.NotEmpty(_viewModel.actuators);
        foreach (var actuator in _viewModel.actuators)
        {
            Assert.Equal(expectedUid, actuator.PCBA.PCBAUid);
        }
    }
}