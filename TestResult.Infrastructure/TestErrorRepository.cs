using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database;
using Infrastructure;
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
        var testResult = await GetTestResultModel(testError.WorkOrderNumber, testError.SerialNumber);
        var testErrorModel = FromDomain(testError);
        testErrorModel.TestResultId = testResult.Id;
        await AddAsync(testErrorModel, testError.GetDomainEvents());
    }

    public async Task<List<TestError>> GetTestErrorsWithFilter(int? woNo, string? tester,
        int? bay, int? errorCode,
        DateTime? startDate, DateTime? endDate)
    {
        var queryBuilder = Query();
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
        return new TestError(model.Id, 0, 0, model.Tester, model.Bay
            , model.ErrorCode, model.ErrorMessage, model.TimeOccured);
    }

    private TestErrorModel FromDomain(TestError testError)
    {
        var testErrorModel = new TestErrorModel()
        {
            Tester = testError.Tester,
            Bay = testError.Bay,
            ErrorCode = testError.ErrorCode,
            ErrorMessage = testError.ErrorMessage,
            TimeOccured = testError.TimeOccured
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
}