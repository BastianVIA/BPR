using Frontend.Entities;

namespace Frontend.Model;

public class TestResultSearchModelModel : ITestResultSearchModel
{
    public async Task<List<TestResult>> GetTestResultsWithFilter(int? woNo)
    {
        return await MockList();

    }

    private async Task<List<TestResult>> MockList()
    {
        return new List<TestResult>()
        {
            new ()
            {
                WorkOrderNumber = 123456,
                SerialNumber = 3,
                Tester = "Tester1",
                Bay = 69,
                MinServoPosition = "132.65 mm",
                MaxServoPosition = "532.55 mm",
                MinBuslinkPosition = "111 mm",
                MaxBuslinkPosition = "700 mm",
                ServoStroke = "69.420 mm"
            },
            new ()
            {
                WorkOrderNumber = 4735,
                SerialNumber = 6,
                Tester = "Tester1",
                Bay = 69,
                MinServoPosition = "132.65 mm",
                MaxServoPosition = "532.55 mm",
                MinBuslinkPosition = "111 mm",
                MaxBuslinkPosition = "700 mm",
                ServoStroke = "69.420 mm"
            },
            new ()
            {
                WorkOrderNumber = 123456,
                SerialNumber = 3,
                Tester = "Tester1",
                Bay = 69,
                MinServoPosition = "132.65 mm",
                MaxServoPosition = "532.55 mm",
                MinBuslinkPosition = "111 mm",
                MaxBuslinkPosition = "700 mm",
                ServoStroke = "69.420 mm"
            },
            new ()
            {
                WorkOrderNumber = 123456,
                SerialNumber = 3,
                Tester = "Tester1",
                Bay = 69,
                MinServoPosition = "132.65 mm",
                MaxServoPosition = "532.55 mm",
                MinBuslinkPosition = "111 mm",
                MaxBuslinkPosition = "700 mm",
                ServoStroke = "69.420 mm"
            },
            new ()
            {
                WorkOrderNumber = 123456,
                SerialNumber = 3,
                Tester = "Tester1",
                Bay = 69,
                MinServoPosition = "132.65 mm",
                MaxServoPosition = "532.55 mm",
                MinBuslinkPosition = "111 mm",
                MaxBuslinkPosition = "700 mm",
                ServoStroke = "69.420 mm"
            }
        };
    }
}