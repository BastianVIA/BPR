using Frontend.Model;
using Frontend.Networking;
using Frontend.Service;

namespace Frontend.UnitTest.Model;

public class UnitTestSearchActuatorModel
{
    private readonly INetwork mockNetwork = Substitute.For<INetwork>();
    private readonly ActuatorDetailsModel model;
    private readonly IFixture fixture = new Fixture();

    public UnitTestSearchActuatorModel()
    {
        model = new ActuatorDetailsModel(mockNetwork);
    }

    [Fact]
    public async Task GetActuatorDetails_ReturnsDefaultActuator_WhenNetworkResponseIsNull()
    {
        // Arrange
        int woNo = fixture.Create<int>();
        int serialNo = fixture.Create<int>();
        
        mockNetwork.GetActuatorDetails(Arg.Any<int>(),Arg.Any<int>()).ReturnsNull();

        // Act
        var actuator = await model.GetActuatorDetails(woNo, serialNo);

        // Assert
        Assert.Null(actuator);
    }

    [Fact]
    public async Task GetActuatorDetails_ReturnsPopulatedActuator_WhenNetworkResponseIsNotNull()
    {
        // Arrange
        int woNo = fixture.Create<int>();
        int serialNo = fixture.Create<int>();
        int pcbaUid = fixture.Create<int>();

        var response = new GetActuatorDetailsResponse
        {
            PcbaUid = pcbaUid
        };

        mockNetwork.GetActuatorDetails(woNo, serialNo).Returns(response);

        // Act
        var actuator = await model.GetActuatorDetails(woNo, serialNo);

        // Assert
        Assert.Equal(woNo, actuator.WorkOrderNumber);
        Assert.Equal(serialNo, actuator.SerialNumber);
        Assert.Equal(pcbaUid, actuator.PCBA.PCBAUid);
    }

    [Fact]
    public async Task GetActuatorDetails_CallsNetworkWithCorrectArguments()
    {
        // Arrange
        int woNo = fixture.Create<int>();
        int serialNo = fixture.Create<int>();

        // Act
        await model.GetActuatorDetails(woNo, serialNo);

        // Assert
        await mockNetwork.Received().GetActuatorDetails(woNo, serialNo);
    }
}
