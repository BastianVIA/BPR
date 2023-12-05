using System.Diagnostics;
using BuildingBlocks.Domain;
using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.Database;

public class BaseRepository<TEntity> where TEntity : class
{
    private ApplicationDbContext _dbContext;
    private IScheduler _scheduler;
    protected BaseRepository(ApplicationDbContext dbContext, IScheduler scheduler)
    {
        _dbContext = dbContext;
        _scheduler = scheduler;
    }

    protected IQueryable<TEntity> Query()
    {
        return _dbContext.Set<TEntity>().AsQueryable();
    }

    protected IQueryable<T> QueryOtherLocal<T>() where T : class
    {
        return _dbContext.Set<T>().Local.AsQueryable();
    }
    
    protected IQueryable<T> QueryOther<T>() where T : class
    {
        return _dbContext.Set<T>().AsQueryable();
    }
    
    protected async Task AddAsync(TEntity entity, IList<IDomainEvent> domainEvents, CancellationToken token = default)
    {
        try
        {
            _scheduler.QueueEvents(domainEvents);
            await _dbContext.Set<TEntity>().AddAsync(entity, token);
        }
        catch (DbUpdateException e)
        {
            throw new AlreadyExistingException();
        }
    }

    protected async Task UpdateAsync(TEntity entity, IList<IDomainEvent> domainEvents, CancellationToken token = default)
    {
        _scheduler.QueueEvents(domainEvents);
        _dbContext.Set<TEntity>().Update(entity);
    }
}