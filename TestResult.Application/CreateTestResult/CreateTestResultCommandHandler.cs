using BuildingBlocks.Application;
using BuildingBlocks.Infrastructure.Database.Transaction;
using TestResult.Domain.RepositoryInterfaces;

namespace TestResult.Application.CreateTestResult;

public class CreateTestResultCommandHandler : ICommandHandler<CreateTestResultCommand>
{
    private ITestResultRepository _testResultRepository;
    private IDbTransaction _dbTransaction;

    public CreateTestResultCommandHandler(ITestResultRepository testResultRepository, IDbTransaction dbTransaction)
    {
        _testResultRepository = testResultRepository;
        _dbTransaction = dbTransaction;
    }

    public async Task Handle(CreateTestResultCommand request, CancellationToken cancellationToken)
    {
        var testResult = Domain.Entities.TestResult.Create(request.WorkOrderNumber, request.SerialNumber, request.Tester,
            request.Bay, request.MinServoPosition, request.MaxServoPosition, request.MinBuslinkPosition, 
            request.MaxBuslinkPosition, request.ServoStroke, request.TimeOccured);
        
        await _testResultRepository.CreateTestResult(testResult);
        await _dbTransaction.CommitAsync(cancellationToken);
    }
}