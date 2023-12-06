using Frontend.Entities;

namespace Frontend.Model;

public class TestResultSearchModelModel : ITestResultSearchModel
{
    public async Task<List<TestResult>> GetTestResultsWithFilter(int? woNo, int? serialNo, string? tester, int? bay)
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
                TestErrors = new()
                {
                    new TestError()
                    {
                        Tester = "Din mor",
                        Bay = 42,
                        ErrorCode = 40,
                        ErrorMessage = "Sygt tyk mor alligevel",
                        TimeOccured = new DateTime(2023, 08,09, 11,35,02)
                    },
                    new TestError()
                    {
                        Tester = "Din søster",
                        Bay = 13,
                        ErrorCode = 4,
                        ErrorMessage = "Lange patter",
                        TimeOccured = new DateTime(2023, 08,09, 11,35,02)
                    }
                },
                
                MaxServoPosition = "532.55 mm",
                MinBuslinkPosition = "111 mm",
                MaxBuslinkPosition = "700 mm",
                ServoStroke = "69.420 mm",
                TimeOccured = new DateTime(2023, 08,09, 11,35,02)
            },
            new ()
            {
                WorkOrderNumber = 4735,
                SerialNumber = 6,
                Tester = "Tester1",
                Bay = 69,
                TestErrors = new(),
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
                TestErrors = new(),
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
                TestErrors = new()
                {
                    new TestError(),
                    new TestError()
                },
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
                TestErrors = new(),
                MinServoPosition = "132.65 mm",
                MaxServoPosition = "532.55 mm",
                MinBuslinkPosition = "111 mm",
                MaxBuslinkPosition = "700 mm",
                ServoStroke = "69.420 mm"
            }
        };
    }
}