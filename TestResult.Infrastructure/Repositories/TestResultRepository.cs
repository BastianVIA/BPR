using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database;
using BuildingBlocks.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using TestResult.Domain.Entities;
using TestResult.Domain.RepositoryInterfaces;

namespace TestResult.Infrastructure.Repositories;

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
        string? tester, int? bay, DateTime? startDate, DateTime? endDate)
    {
        var queryBuilder = Query()
            .Include(t => t.TestErrors)
            .ThenInclude(t => t.ErrorCodeModel)
            .AsQueryable();
        if (woNo != null)
        {
            queryBuilder = queryBuilder.Where(m => m.WorkOrderNumber == woNo);
        }

        if (serialNo != null)
        {
            queryBuilder = queryBuilder.Where(m => m.SerialNumber == serialNo);
        }

        if (tester != null)
        {
            queryBuilder = queryBuilder.Where(m => m.Tester == tester);
        }

        if (bay != null)
        {
            queryBuilder = queryBuilder.Where(m => m.Bay == bay);
        }

        if (startDate is not null)
        {
            queryBuilder = queryBuilder.Where(m => m.TimeOccured > startDate);
        }

        if (endDate is not null)
        {
            queryBuilder = queryBuilder.Where(m => m.TimeOccured < endDate);
        }

        var actuatorTestModels = await queryBuilder.ToListAsync();
        return ToDomain(actuatorTestModels);
    }

    public async Task<int> GetNumberOfTestsPerformedInInterval(DateTime startTime, DateTime endTime)
    {
        return await Query().Where(model => model.TimeOccured > startTime && model.TimeOccured < endTime).CountAsync();
    }

    public async Task<List<string>> GetAllTesters()
    {
        var testeresForResult = await Query().Select(model => model.Tester).Distinct().ToListAsync();

        var testeresForError =
            await QueryOther<TestErrorModel>().Select(model => model.Tester).Distinct().ToListAsync();

        List<string> allTestersIncludingErrors = new();
        allTestersIncludingErrors.AddRange(testeresForResult);
        allTestersIncludingErrors.AddRange(testeresForError);
        
        return allTestersIncludingErrors.Distinct().ToList();
    }

    public async Task<int> GetTotalTestResultAmount()
    {
        return Query().Count();
    }

    public async Task<int> GetTotalTestResultWithErrorsAmount()
    {
        return Query().Include(e => e.TestErrors).Count(e => e.TestErrors.Count > 0);
    }

    public async Task<int> GetTotalTestResultWithoutErrorsAmount()
    {
        return Query().Include(e => e.TestErrors).Count(e => e.TestErrors.Count == 0);
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
        var list = testResultModel.TestErrors.Select(error => new TestError(error.Id, error.Tester, error.Bay, error.ErrorCode, error.ErrorCodeModel.ErrorMessage, error.TimeOccured)).ToList();

        return new Domain.Entities.TestResult(testResultModel.Id, testResultModel.WorkOrderNumber,
            testResultModel.SerialNumber, testResultModel.Tester, testResultModel.Bay, testResultModel.MinServoPosition,
            testResultModel.MaxServoPosition, testResultModel.MinBuslinkPosition, testResultModel.MaxBuslinkPosition,
            testResultModel.ServoStroke, testResultModel.TimeOccured, list);
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