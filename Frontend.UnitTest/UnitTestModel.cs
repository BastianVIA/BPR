using Frontend.Model;
using Frontend.Networking;
using Frontend.Service;
using NSubstitute.ExceptionExtensions;

namespace Frontend.UnitTest;

using Xunit;
using NSubstitute;

public class UnitTestModel
{
    [Fact]
    public async Task GetActuatorDetails_ReturnsDefaultActuator_WhenNetworkResponseIsNull()
    {
        // Arrange
        int woNo = 123312;
        int serialNo = 124111;

        var mockNetwork = Substitute.For<INetwork>();
        mockNetwork.GetActuatorDetails(woNo, serialNo).Returns((GetActuatorDetailsResponse)null);

        var model = new ActuatorDetailsModel(mockNetwork);

        // Act
        var actuator = await model.GetActuatorDetails(woNo, serialNo);

        // Assert
        Assert.Equal(default, actuator.WorkOrderNumber);
        Assert.Equal(default, actuator.SerialNumber);
        Assert.Equal(default, actuator.PCBA.PCBAUid);
    }


    [Fact]
    public async Task GetActuatorDetails_ReturnsPopulatedActuator_WhenNetworkResponseIsNotNull()
    {
        // Arrange
        int woNo = 23423;
        int serialNo = 23122;
        int pcbaUid = 234123;

        var response = new GetActuatorDetailsResponse
        {
            PcbaUid = pcbaUid
        };

        var mockNetwork = Substitute.For<INetwork>();
        mockNetwork.GetActuatorDetails(woNo, serialNo).Returns(response);

        var model = new ActuatorDetailsModel(mockNetwork);

        // Act
        var actuator = await model.GetActuatorDetails(woNo, serialNo);

        // Assert
        Assert.Equal(woNo, actuator.WorkOrderNumber);
        Assert.Equal(serialNo, actuator.SerialNumber);
        Assert.Equal(pcbaUid, actuator.PCBA.PCBAUid);
    }


    [Theory]
    [InlineData(0, 0)]
    [InlineData(-1, -1)]
    [InlineData(int.MaxValue, int.MaxValue)]
    public async Task GetActuatorDetails_HandlesVariousInputValues(int woNo, int serialNo)
    {
        // Arrange
        var mockNetwork = Substitute.For<INetwork>();
        mockNetwork.GetActuatorDetails(woNo, serialNo).Returns((GetActuatorDetailsResponse)null);

        var model = new ActuatorDetailsModel(mockNetwork);

        // Act
        var actuator = await model.GetActuatorDetails(woNo, serialNo);

        // Assert

        Assert.Equal(default, actuator.WorkOrderNumber);
        Assert.Equal(default, actuator.SerialNumber);
        Assert.Equal(default, actuator.PCBA.PCBAUid);
    }


    [Fact]
    public async Task GetActuatorDetails_CallsNetworkWithCorrectArguments()
    {
        // Arrange
        int woNo = 23235;
        int serialNo = 23423;

        var mockNetwork = Substitute.For<INetwork>();
        var model = new ActuatorDetailsModel(mockNetwork);

        // Act
        await model.GetActuatorDetails(woNo, serialNo);

        // Assert
        await mockNetwork.Received().GetActuatorDetails(woNo, serialNo);
    }
}