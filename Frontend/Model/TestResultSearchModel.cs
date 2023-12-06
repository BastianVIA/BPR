using Frontend.Entities;
using Frontend.Networking;

namespace Frontend.Model;

public class TestResultSearchModelModel : ITestResultSearchModel
{
    private INetwork _network;

    public TestResultSearchModelModel(INetwork network)
    {
        _network = network;
    }
    public async Task<List<TestResult>> GetTestResultsWithFilter(int? woNo, int? serialNo, string? tester, int? bay)
    {
        var response = await _network.GetTestResultWithFilter(woNo, serialNo, tester, bay);
        var list = new List<TestResult>();
        foreach (var testResult in response.ActuatorTest)
        {
            list.Add(new TestResult()
            {
                WorkOrderNumber = testResult.WorkOrderNumber,
                SerialNumber = testResult.WorkOrderNumber,
                Bay = testResult.Bay,
                Tester = testResult.Tester,
                MaxServoPosition = testResult.MaxServoPosition,
                MaxBuslinkPosition = testResult.MaxBuslinkPosition,
                MinBuslinkPosition = testResult.MinBuslinkPosition,
                MinServoPosition = testResult.MinServoPosition,
                ServoStroke = testResult.ServoStroke,
                TimeOccured = testResult.TimeOccured,
                TestErrors = new List<TestError>()
            });
            
        }

        return list;
    }
}