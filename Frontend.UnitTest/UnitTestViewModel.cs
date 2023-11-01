using NSubstitute.ExceptionExtensions;

namespace Frontend.UnitTest;

using Xunit;
using NSubstitute;
using System.Threading.Tasks;
using Frontend.Pages;
using Frontend.Entities;
using Frontend.Model;
using AutoFixture;

public class UnitTestViewModel
{
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task SearchActuator_SetsActuatorAndPcbaUidCorrectly()
    {
        // Arrange
        var mockActuatorDetailsModel = Substitute.For<IActuatorDetailsModel>();
        var expectedActuator = _fixture.Create<Actuator>().WithPCBAUid(_fixture.Create<int>());

        mockActuatorDetailsModel.GetActuatorDetails(Arg.Any<int>(), Arg.Any<int>())
            .Returns(expectedActuator);
        var component = new PCBAInfoBase(mockActuatorDetailsModel);

        // Act
        await component.SearchActuator();

        // Assert

        Assert.Equal(expectedActuator.WorkOrderNumber, component.actuator.WorkOrderNumber);
    }


    [Fact]
    public async Task SearchActuator_HandlesNullActuatorCorrectly()
    {
        // Arrange
        var mockActuatorDetailsModel = Substitute.For<IActuatorDetailsModel>();
        mockActuatorDetailsModel.GetActuatorDetails(Arg.Any<int>(), Arg.Any<int>())
            .Returns((Actuator)null);

        var component = new PCBAInfoBase(mockActuatorDetailsModel);

        // Act
        await component.SearchActuator();

        // Assert
        Assert.Null(component.actuator);
    }


    [Fact]
    public async Task SearchActuator_HandlesExceptionsCorrectly()
    {
        // Arrange
        var mockActuatorDetailsModel = Substitute.For<IActuatorDetailsModel>();
        mockActuatorDetailsModel.GetActuatorDetails(Arg.Any<int>(), Arg.Any<int>())
            .Throws(new Exception("Error fetching details"));

        var component = new PCBAInfoBase(mockActuatorDetailsModel);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => component.SearchActuator());
    }
    

    [Fact]
    public async Task SearchActuator_MultipleCalls_DontInterfere()
    {
        // Arrange
        var firstActuator = _fixture.Create<Actuator>();
        var secondActuator = _fixture.Create<Actuator>();
        var mockActuatorDetailsModel = Substitute.For<IActuatorDetailsModel>();

        mockActuatorDetailsModel.GetActuatorDetails(1, Arg.Any<int>()).Returns(firstActuator);
        mockActuatorDetailsModel.GetActuatorDetails(2, Arg.Any<int>()).Returns(secondActuator);

        var component = new PCBAInfoBase(mockActuatorDetailsModel);

        // Act
        async Task ActuatorSearch(int workOrderNumber, Actuator expectedActuator)
        {
            component.actuator.WorkOrderNumber = workOrderNumber;
            await component.SearchActuator();
            var result = component.actuator;

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
        var mockActuatorDetailsModel = Substitute.For<IActuatorDetailsModel>();

        mockActuatorDetailsModel.GetActuatorDetails(1, Arg.Any<int>()).Returns(firstActuator);

        var component = new PCBAInfoBase(mockActuatorDetailsModel);

        // Act
        component.actuator.WorkOrderNumber = 1;
        await component.SearchActuator();

        // Assert
        mockActuatorDetailsModel.Received(1).GetActuatorDetails(1, Arg.Any<int>());
    }
}