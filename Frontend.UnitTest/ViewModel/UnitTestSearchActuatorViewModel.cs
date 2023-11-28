using Frontend.Entities;
using Frontend.Model;
using Frontend.Pages;
using NSubstitute.ExceptionExtensions;

namespace Frontend.UnitTest.ViewModel;

public class UnitTestSearchActuatorViewModel
{
    private readonly Fixture _fixture = new();
    private readonly IActuatorDetailsModel _mockActuatorDetailsModel = Substitute.For<IActuatorDetailsModel>();
    private readonly PCBAInfoBase _viewModel;

    public UnitTestSearchActuatorViewModel()
    {
        _viewModel = new PCBAInfoBase(_mockActuatorDetailsModel);
    }

    [Fact]
    public async Task SearchActuator_SetsActuatorAndPcbaUidCorrectly()
    {
        // Arrange
        var expectedActuator = _fixture.Create<Actuator>().WithPCBAUid(_fixture.Create<int>());

        _mockActuatorDetailsModel.GetActuatorDetails(Arg.Any<int>(), Arg.Any<int>())
            .Returns(expectedActuator);

        // Act
        await _viewModel.SearchActuator();

        // Assert

        Assert.Equal(expectedActuator.WorkOrderNumber, _viewModel.actuator.WorkOrderNumber);
    }


    [Fact]
    public async Task SearchActuator_HandlesNullActuatorCorrectly()
    {
        // Arrange
        _mockActuatorDetailsModel.GetActuatorDetails(Arg.Any<int>(), Arg.Any<int>())
            .ReturnsNull();


        // Act
        await _viewModel.SearchActuator();

        // Assert
        Assert.Null(_viewModel.actuator);
    }


    [Fact]
    public async Task SearchActuator_HandlesExceptionsCorrectly()
    {
        // Arrange
        _mockActuatorDetailsModel.GetActuatorDetails(Arg.Any<int>(), Arg.Any<int>())
            .Throws<Exception>();


        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _viewModel.SearchActuator());
    }


    [Fact]
    public async Task SearchActuator_MultipleCalls_DontInterfere()
    {
        // Arrange
        var firstActuator = _fixture.Create<Actuator>();
        var secondActuator = _fixture.Create<Actuator>();

        _mockActuatorDetailsModel.GetActuatorDetails(1, Arg.Any<int>()).Returns(firstActuator);
        _mockActuatorDetailsModel.GetActuatorDetails(2, Arg.Any<int>()).Returns(secondActuator);


        // Act
        async Task ActuatorSearch(int workOrderNumber, Actuator expectedActuator)
        {
            _viewModel.actuator.WorkOrderNumber = workOrderNumber;
            await _viewModel.SearchActuator();
            var result = _viewModel.actuator;

            // Assert
            Assert.Equal(expectedActuator.WorkOrderNumber, result.WorkOrderNumber);
        }

        // concurrent call test
        var task1 = ActuatorSearch(1, firstActuator);
        var task2 = ActuatorSearch(2, secondActuator);

        await Task.WhenAll(task1, task2);
    }


    [Fact]
    public async Task SearchActuator_CalledOnce()
    {
        // Arrange
        var firstActuator = _fixture.Create<Actuator>();

        _mockActuatorDetailsModel.GetActuatorDetails(1, Arg.Any<int>()).Returns(firstActuator);


        // Act
        _viewModel.actuator.WorkOrderNumber = 1;
        await _viewModel.SearchActuator();

        // Assert
        await _mockActuatorDetailsModel.Received(1).GetActuatorDetails(1, Arg.Any<int>());
    }
}