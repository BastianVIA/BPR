using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using TestResult.Domain.Entities;

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

    public async Task<List<Domain.Entities.TestResult>> GetActuatorsTestDetails(int? woNo, int? serialNo,
        string? tester, int? bay)
    {
        var queryBuilder = Query();
        if (woNo != null)
        {
            queryBuilder = queryBuilder.Where(m => m.WorkOrderNumber == woNo.Value);
        }

        if (serialNo != null)
        {
            queryBuilder = queryBuilder.Where(m => m.SerialNumber == serialNo.Value);
        }

        if (tester != null)
        {
            queryBuilder = queryBuilder.Where(m => m.Tester == tester);
        }

        if (bay != null)
        {
            queryBuilder = queryBuilder.Where(m => m.Bay == bay);
        }


        var actuatorTestModels = await queryBuilder.ToListAsync();
        return ToDomain(actuatorTestModels);
    }

    private List<Domain.Entities.TestResult> ToDomain(List<TestResultModel> testResultModels)
    {
        List<Domain.Entities.TestResult> domainTestResults = new List<Domain.Entities.TestResult>();
        foreach (var testResultModel in testResultModels)
        {
            domainTestResults.Add(ToDomain(testResultModel));
        }

        return domainTestResults;
    }

    private Domain.Entities.TestResult ToDomain(TestResultModel testResultModel)
    {
        return new Domain.Entities.TestResult(testResultModel.Id, testResultModel.WorkOrderNumber,
            testResultModel.SerialNumber, testResultModel.Tester, testResultModel.Bay, testResultModel.MinServoPosition,
            testResultModel.MaxServoPosition, testResultModel.MinBuslinkPosition, testResultModel.MaxBuslinkPosition,
            testResultModel.ServoStroke, testResultModel.TimeOccured);
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
            ServoStroke = testResult.ServoStroke,
            TimeOccured = testResult.TimeOccured
        };
        return testResultModel;
    }
}