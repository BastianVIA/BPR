using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.Database;

public class BaseRepository<TEntity> where TEntity : class
{

    protected ApplicationDbContext _dbContext;

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
        try
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.ChangeTracker.Clear();
        }
        catch (DbUpdateException e)
        {
            throw new AlreadyExistingException();
        }
    }

    public async Task Update(TEntity entity, CancellationToken token = default)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}