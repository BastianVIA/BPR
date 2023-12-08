using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database;
using BuildingBlocks.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;
using TestResult.Domain.Entities;
using TestResult.Domain.Repositories;

namespace TestResult.Infrastructure;

public class TestErrorRepository : BaseRepository<TestErrorModel>, ITestErrorRepository
{
    public TestErrorRepository(ApplicationDbContext dbContext, IScheduler scheduler) : base(dbContext, scheduler)
    {
    }

    public async Task CreateTestError(TestError testError)
    {
        if (testError.WorkOrderNumber == null || testError.SerialNumber == null)
        {
            throw new ArgumentException("Need to have WrokOrderNumber and SerialNumber for creation");
        }

        var testResult = await GetTestResultModel(testError.WorkOrderNumber.Value, testError.SerialNumber.Value);
        var errorCode = await GetTestErrorCodeModel(testError.ErrorCode);
        var testErrorModel = FromDomain(testError);
        testErrorModel.TestResultId = testResult.Id;
        testErrorModel.ErrorCodeModel = errorCode;
        await AddAsync(testErrorModel, testError.GetDomainEvents());
    }

    public async Task<List<TestError>> GetTestErrorsWithFilter(int? woNo, string? tester,
        int? bay, int? errorCode,
        DateTime? startDate, DateTime? endDate)
    {
        var queryBuilder = Query()
            .Include(model => model.ErrorCodeModel)
            .AsQueryable();
        if (woNo != null)
        {
            var testResultIds = await QueryOther<TestResultModel>()
                .Where(model => model.WorkOrderNumber == woNo).Select(model => model.Id).ToListAsync();

            queryBuilder = queryBuilder.Where(model => testResultIds.Contains(model.TestResultId));
        }

        if (tester != null)
        {
            queryBuilder = queryBuilder.Where(model => model.Tester == tester);
        }

        if (bay != null)
        {
            queryBuilder = queryBuilder.Where(model => model.Bay == bay);
        }

        if (errorCode != null)
        {
            queryBuilder = queryBuilder.Where(model => model.ErrorCode == errorCode);
        }

        if (errorCode != null)
        {
            queryBuilder = queryBuilder.Where(model => model.TimeOccured > startDate);
        }

        if (errorCode != null)
        {
            queryBuilder = queryBuilder.Where(model => model.TimeOccured < endDate);
        }

        queryBuilder = queryBuilder.OrderBy(model => model.TimeOccured);

        var errorModels = await queryBuilder.ToListAsync();
        return ToDomain(errorModels);
    }

    public async Task<List<TestError>> GetAllErrorsForTesterSince(List<string> testers, DateTime startDate,
        DateTime endDate)
    {
        var testErrors = await Query().Where(model => model.TimeOccured > startDate && model.TimeOccured < endDate)
            .Where(model => testers.Contains(model.Tester))
            .ToListAsync();
        return ToDomain(testErrors);
    }

    private List<TestError> ToDomain(List<TestErrorModel> models)
    {
        List<TestError> domains = new();
        foreach (var model in models)
        {
            domains.Add(ToDomain(model));
        }

        return domains;
    }

    private TestError ToDomain(TestErrorModel model)
    {
        return new TestError(model.Id, model.Tester, model.Bay
            , model.ErrorCode, model.ErrorCodeModel.ErrorMessage, model.TimeOccured);
    }

    private TestErrorModel FromDomain(TestError testError)
    {
        var errorCodeMode = new TestErrorCodeModel()
        {
            ErrorCode = testError.ErrorCode,
            ErrorMessage = testError.ErrorMessage
        };
        var testErrorModel = new TestErrorModel()
        {
            Tester = testError.Tester,
            Bay = testError.Bay,
            ErrorCode = testError.ErrorCode,
            TimeOccured = testError.TimeOccured,
            ErrorCodeModel = errorCodeMode,
        };
        return testErrorModel;
    }

    private async Task<TestResultModel> GetTestResultModel(int workOrderNo, int serialNo)
    {
        var testResult = QueryOtherLocal<TestResultModel>().FirstOrDefault(
                             t => t.WorkOrderNumber == workOrderNo && t.SerialNumber == serialNo)
                         ?? await QueryOther<TestResultModel>().FirstAsync(
                             t => t.WorkOrderNumber == workOrderNo && t.SerialNumber == serialNo);

        return testResult;
    }
    
    private async Task<TestErrorCodeModel> GetTestErrorCodeModel(int errorCode)
    {
        var testErrorCodeModel = await QueryOtherLocal<TestErrorCodeModel>()
            .FirstOrDefaultAsync(e => e.ErrorCode == errorCode);
        if (testErrorCodeModel == null)
        {
            testErrorCodeModel = await QueryOther<TestErrorCodeModel>().FirstAsync(e => e.ErrorCode == errorCode);
        }

        return testErrorCodeModel;
    }
}