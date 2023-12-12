using Frontend.Entities;
using Frontend.Model;
using Frontend.Pages;
using Frontend.Service.AlertService;

namespace Frontend.UnitTest.ViewModel;

public class ActuatorSearchBaseTests
{
    private Fixture _fixture = new();
    private IActuatorSearchModel _model = Substitute.For<IActuatorSearchModel>();
    private IActuatorSearchCsvModel _csvModel = Substitute.For<IActuatorSearchCsvModel>();
    private IAlertService _alertService = Substitute.For<IAlertService>();
    private ActuatorSearchBase _viewModel;

    public ActuatorSearchBaseTests()
    {
        _viewModel = new ActuatorSearchBase(_model, _csvModel, _alertService);
    }

    [Fact]
    public async Task SearchActuators_ShouldSetActuatorListEmpty_WhenNoMatchesFound()
    {
        var expected = new List<Actuator>();

        _model.GetActuatorWithFilter(Arg.Any<int?>(), 
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
            .Returns(expected);
        
        await _viewModel.SearchActuators();
        
        Assert.True(_viewModel.Actuators.Count == 0);
    }

    [Fact]
    public async Task SearchActuators_ShouldReturnOneActuator_WhenOneMatchFound()
    {
        // Arrange
        var expected = new List<Actuator>
        {
            _fixture.Create<Actuator>()
        };

        _model.GetActuatorWithFilter(Arg.Any<int?>(), 
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
            .Returns(expected);
        
        // Act
        await _viewModel.SearchActuators();
        
        // Assert
        Assert.NotEmpty(_viewModel.Actuators);
        Assert.True(_viewModel.Actuators.Count == 1);
    }

    [Fact]
    public async Task SearchActuators_ShouldReturnActuatorWithExactUid_WhenMatchFound()
    {
        // Arrange
        var expectedUid = _fixture.Create<string>();
        _viewModel.SearchActuator.PCBAUid = expectedUid;
        var expectedList = new List<Actuator>
        {
            _fixture.Create<Actuator>().WithPCBAUid(expectedUid)
        };
        
        _model.GetActuatorWithFilter(Arg.Any<int?>(), 
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
            .Returns(expectedList);
        
        // Act
        await _viewModel.SearchActuators();
        
        // Assert
        Assert.NotEmpty(_viewModel.Actuators);
        Assert.Equal(expectedUid, _viewModel.Actuators.First().PCBA.PCBAUid);
    }

    [Fact]
    public async Task SearchActuators_ShouldReturnManyActuators_WhenMultipleMatchesFound()
    {
        // Arrange
        var expectedList = _fixture.CreateMany<Actuator>().ToList();

        _model.GetActuatorWithFilter(Arg.Any<int?>(), 
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
            .Returns(expectedList);

        // Act
        await _viewModel.SearchActuators();
        
        // Assert
        Assert.NotEmpty(_viewModel.Actuators);
        Assert.True(_viewModel.Actuators.Count > 1);
    }

    [Fact]
    public async Task SearchActuators_ShouldReturnManyCorrectActuators_WhenMultipleMatchesFound()
    {
        // Arrange
        var expectedUid = _fixture.Create<string>();
        _viewModel.SearchActuator.PCBAUid = expectedUid;

        var expectedList = new List<Actuator>
        {
            _fixture.Create<Actuator>().WithPCBAUid(expectedUid),
            _fixture.Create<Actuator>().WithPCBAUid(expectedUid),
            _fixture.Create<Actuator>().WithPCBAUid(expectedUid)
        };
        
        _model.GetActuatorWithFilter(Arg.Any<int?>(), 
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
            .Returns(expectedList);

        // Act
        await _viewModel.SearchActuators();
        
        // Assert
        Assert.NotEmpty(_viewModel.Actuators);
        foreach (var actuator in _viewModel.Actuators)
        {
            Assert.Equal(expectedUid, actuator.PCBA.PCBAUid);
        }
    }
}