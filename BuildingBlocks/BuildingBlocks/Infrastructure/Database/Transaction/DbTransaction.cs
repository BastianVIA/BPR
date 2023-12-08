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

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _scheduler.PublishEvents();
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}