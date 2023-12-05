using Frontend.Entities;
using Frontend.Model;
using Frontend.Pages;

namespace Frontend.UnitTest.ViewModel;

public class ActuatorInfoTests
{
    private Fixture _fixture = new();
    private IActuatorSearchModel _model = Substitute.For<IActuatorSearchModel>();
    private ActuatorSearchBase _viewModel;

    public ActuatorInfoTests()
    {
        _viewModel = new ActuatorSearchBase(_model);
    }

    [Fact]
    public async Task SearchActuators_ShouldReturnEmptyList_WhenNoMatchesFound()
    {
        var expected = new List<Actuator>();

        _model.GetActuatorWithFilter(Arg.Any<int>(), 
                Arg.Any<int>(), 
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<int>())
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

        _model.GetActuatorWithFilter(Arg.Any<int>(), 
                Arg.Any<int>(), 
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<int>())
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
        
        _model.GetActuatorWithFilter(Arg.Any<int>(), 
                Arg.Any<int>(), 
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<int>())
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

        _model.GetActuatorWithFilter(Arg.Any<int>(), 
                Arg.Any<int>(), 
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<int>())
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
        
        _model.GetActuatorWithFilter(Arg.Any<int>(), 
                Arg.Any<int>(), 
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<int>(),
                Arg.Any<int>())
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