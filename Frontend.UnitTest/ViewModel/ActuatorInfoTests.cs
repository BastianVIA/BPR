using Frontend.Entities;
using Frontend.Model;
using Frontend.Pages;

namespace Frontend.UnitTest.ViewModel;

public class ActuatorInfoTests
{
    private Fixture _fixture = new();
    private IActuatorDetailsModel _model = Substitute.For<IActuatorDetailsModel>();
    private ActuatorInfoBase _viewModel;

    public ActuatorInfoTests()
    {
        _viewModel = new ActuatorInfoBase(_model);
    }

    [Fact]
    public async Task SearchPcba_ShouldReturnEmptyList_WhenNoMatchesFound()
    {
        var expected = new List<Actuator>();

        _model.GetActuatorsByUid(Arg.Any<string>())
            .Returns(expected);
        
        await _viewModel.SearchPcba();
        
        Assert.True(_viewModel.actuators.Count == 0);
    }
}