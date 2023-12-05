using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database;
using Infrastructure;
using TestResult.Domain.Repositories;

namespace TestResult.Infrastructure;

public class TestResultRepository : BaseRepository<TestResultModel>, ITestResultRepository
{
    public TestResultRepository(ApplicationDbContext dbContext, IScheduler scheduler) : base(dbContext, scheduler)
    {
    }

    public async Task CreateTestResult(Domain.Entities.TestResult testResult)
    {
        await AddAsync(FromDomain(testResult), testResult.GetDomainEvents());
    }

    public async Task<Domain.Entities.TestResult> GetTestResult(int woNo, int serialNo)
    {
        throw new NotImplementedException();
    }
    
    private Domain.Entities.TestResult ToDomain(Domain.Entities.TestResult testResultModel)
    {
        return new Domain.Entities.TestResult(testResultModel.Id, testResultModel.WorkOrderNumber, 
            testResultModel.SerialNumber, testResultModel.Tester, testResultModel.Bay, testResultModel.MinServoPosition,
            testResultModel.MaxServoPosition, testResultModel.MinBuslinkPosition, testResultModel.MaxBuslinkPosition,
            testResultModel.ServoStroke);
    }
    
    private TestResultModel FromDomain(Domain.Entities.TestResult testResult)
    {
        var testResultModel = new TestResultModel()
        {
            WorkOrderNumber = testResult.WorkOrderNumber,
            SerialNumber = testResult.SerialNumber,
            Tester = testResult.Tester,
            Bay = testResult.Bay,
            MinServoPosition = testResult.MinServoPosition,
            MaxServoPosition = testResult.MaxServoPosition,
            MinBuslinkPosition = testResult.MinBuslinkPosition,
            MaxBuslinkPosition = testResult.MaxBuslinkPosition,
            ServoStroke = testResult.ServoStroke
        };
        return testResultModel;
    }
}