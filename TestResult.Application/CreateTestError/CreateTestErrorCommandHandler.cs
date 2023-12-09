using BuildingBlocks.Application;
using BuildingBlocks.Infrastructure.Database.Transaction;
using TestResult.Domain.Entities;
using TestResult.Domain.RepositoryInterfaces;

namespace TestResult.Application.CreateTestError;

public class CreateTestErrorCommandHandler : ICommandHandler<CreateTestErrorCommand>
{
    private ITestErrorRepository _testErrorRepository;
    private IDbTransaction _dbTransaction;

    public CreateTestErrorCommandHandler(ITestErrorRepository testErrorRepository, IDbTransaction dbTransaction)
    {
        _testErrorRepository = testErrorRepository;
        _dbTransaction = dbTransaction;
    }

    public async Task Handle(CreateTestErrorCommand request, CancellationToken cancellationToken)
    {
        var testError = TestError.Create(request.WorkOrderNumber, request.SerialNumber, request.Tester, request.Bay,
            request.ErrorCode, request.ErrorMessage, request.TimeOccured);
        
        await _testErrorRepository.CreateTestError(testError);
        await _dbTransaction.CommitAsync(cancellationToken);
    }
}