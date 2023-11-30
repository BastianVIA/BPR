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

        _model.GetActuatorsByPCBA(Arg.Any<string>())
            .Returns(expected);
        
        await _viewModel.SearchActuators();
        
        Assert.True(_viewModel.actuators.Count == 0);
    }

    [Fact]
    public async Task SearchActuators_ShouldReturnOneActuator_WhenOneMatchFound()
    {
        // Arrange
        var expected = new List<Actuator>
        {
            _fixture.Create<Actuator>()
        };

        _model.GetActuatorsByPCBA(Arg.Any<string>())
            .Returns(expected);
        
        // Act
        await _viewModel.SearchActuators();
        
        // Assert
        Assert.NotEmpty(_viewModel.actuators);
        Assert.True(_viewModel.actuators.Count == 1);
    }

    [Fact]
    public async Task SearchActuators_ShouldReturnActuatorWithExactUid_WhenMatchFound()
    {
        // Arrange
        var expectedUid = _fixture.Create<string>();
        _viewModel.SearchActuator.PCBA.PCBAUid = expectedUid;
        var expectedList = new List<Actuator>
        {
            _fixture.Create<Actuator>().WithPCBAUid(expectedUid)
        };
        
        _model.GetActuatorsByPCBA(expectedUid)
            .Returns(expectedList);
        
        // Act
        await _viewModel.SearchActuators();
        
        // Assert
        Assert.NotEmpty(_viewModel.actuators);
        Assert.Equal(expectedUid, _viewModel.actuators.First().PCBA.PCBAUid);
    }

    [Fact]
    public async Task SearchActuators_ShouldReturnManyActuators_WhenMultipleMatchesFound()
    {
        // Arrange
        var expectedList = _fixture.CreateMany<Actuator>().ToList();

        _model.GetActuatorsByPCBA(Arg.Any<string>())
            .Returns(expectedList);

        // Act
        await _viewModel.SearchActuators();
        
        // Assert
        Assert.NotEmpty(_viewModel.actuators);
        Assert.True(_viewModel.actuators.Count > 1);
    }

    [Fact]
    public async Task SearchActuators_ShouldReturnManyCorrectActuators_WhenMultipleMatchesFound()
    {
        // Arrange
        var expectedUid = _fixture.Create<string>();
        _viewModel.SearchActuator.PCBA.PCBAUid = expectedUid;

        var expectedList = new List<Actuator>
        {
            _fixture.Create<Actuator>().WithPCBAUid(expectedUid),
            _fixture.Create<Actuator>().WithPCBAUid(expectedUid),
            _fixture.Create<Actuator>().WithPCBAUid(expectedUid)
        };
        
        _model.GetActuatorsByPCBA(expectedUid)
            .Returns(expectedList);

        // Act
        await _viewModel.SearchActuators();
        
        // Assert
        Assert.NotEmpty(_viewModel.actuators);
        foreach (var actuator in _viewModel.actuators)
        {
            Assert.Equal(expectedUid, actuator.PCBA.PCBAUid);
        }
    }
}