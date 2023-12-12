namespace BuildingBlocks.Infrastructure.Database.Transaction;

public interface IDbTransaction
{
    public Task CommitAsync(CancellationToken cancellationToken);
}