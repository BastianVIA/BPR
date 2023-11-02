using BuildingBlocks.Infrastructure.Database;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class PCBARepository : BaseRepository<PCBAModel>, IPCBARepository
{
    public PCBARepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task CreatePCBA(PCBA pcba)
    {
        var existingEntity = _dbContext.Set<PCBAModel>().Local.FirstOrDefault(e => e.Uid == pcba.Uid);
        if (existingEntity != null)
        {
            _dbContext.Entry(existingEntity).State = EntityState.Detached;
            _dbContext.Entry(pcba).State = EntityState.Added;
        }
        await Add(FromDomain(pcba));
    }

    public async Task<PCBA> GetPCBA(string id)
    {
        var pcbaModel = await Query().AsNoTracking().FirstOrDefaultAsync(p => p.Uid == id);
        if (pcbaModel == null)
        {
            throw new KeyNotFoundException(
                $"Could not find PCBA with Uid: {id}");
        }
        return ToDomain(pcbaModel);
    }

    public async Task UpdatePCBA(PCBA pcba)
    {
        await Update(FromDomain(pcba));
    }

    private PCBA ToDomain(PCBAModel pcbaModel)
    {
        return new PCBA(pcbaModel.Uid, pcbaModel.ManufacturerNumber);
    }
    
    private PCBAModel FromDomain(PCBA pcba)
    {
        var pcbaModel = new PCBAModel()
        {
           Uid = pcba.Uid,
           ManufacturerNumber = pcba.ManufacturerNumber
        };
        return pcbaModel;
    }
}