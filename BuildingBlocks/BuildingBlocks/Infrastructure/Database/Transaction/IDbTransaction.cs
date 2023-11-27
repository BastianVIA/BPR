namespace BuildingBlocks.Infrastructure.Database.Transaction;

public interface IDbTransaction
{
    Task CommitAsync(CancellationToken cancellationToken);
}