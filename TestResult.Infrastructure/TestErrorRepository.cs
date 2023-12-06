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