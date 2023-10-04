namespace BuildingBlocks.Infrastructure.Database;

public class BaseRepository<TEntity> where TEntity : class
{

    private ApplicationDbContext _dbContext;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> Query()
    {
        return _dbContext.Set<TEntity>().AsQueryable();
    }

    public async Task Add(TEntity entity, CancellationToken token = default)
    {
        _dbContext.Set<TEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();

    }
}