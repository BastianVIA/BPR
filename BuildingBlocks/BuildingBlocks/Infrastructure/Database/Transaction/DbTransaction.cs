namespace BuildingBlocks.Infrastructure.Database.Transaction;

public class DbTransaction : IDbTransaction
{
    private ApplicationDbContext _dbContext;
    private IScheduler _scheduler;

    public DbTransaction(ApplicationDbContext dbContext, IScheduler scheduler)
    {
        _dbContext = dbContext;
        _scheduler = scheduler;
    }

    public Task CommitAsync(CancellationToken cancellationToken)
    {
        _scheduler.PublishEvents();
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}