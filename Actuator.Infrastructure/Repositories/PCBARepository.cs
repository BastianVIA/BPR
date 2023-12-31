using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database;
using BuildingBlocks.Infrastructure.Database.Models;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class PCBARepository : BaseRepository<PCBAModel>, IPCBARepository
{
    public PCBARepository(ApplicationDbContext dbContext, IScheduler scheduler) : base(dbContext, scheduler)
    {
    }

    public async Task CreatePCBA(PCBA pcba)
    {
        await AddAsync(FromDomain(pcba), pcba.GetDomainEvents());
    }

    public async Task<PCBA> GetPCBA(string id)
    {
        var pcbaModel = await Query()
                            .AsNoTracking()
                            .FirstOrDefaultAsync(p => p.Uid == id) 
                        ?? QueryOtherLocal<PCBAModel>().FirstOrDefault(p => p.Uid == id);

        if (pcbaModel == null)
        {
            throw new KeyNotFoundException($"Could not find PCBA with Uid: {id}");
        }

        return ToDomain(pcbaModel);
    }

    public async Task UpdatePCBA(PCBA pcba)
    {
        var pcbaToUpdate = Query().FirstOrDefault(p => p.Uid == pcba.Uid);
        if (pcbaToUpdate == null)
        {
            throw new KeyNotFoundException(
                $"Could not find PCBA with Uid: {pcba.Uid} to update");
        }
        await UpdateAsync(FromDomain(pcba), pcba.GetDomainEvents());
    }

    private PCBA ToDomain(PCBAModel pcbaModel)
    {
        return new PCBA(pcbaModel.Uid, pcbaModel.ManufacturerNumber, pcbaModel.ItemNumber, pcbaModel.Software, pcbaModel.ProductionDateCode, pcbaModel.ConfigNo);
    }
    
    private PCBAModel FromDomain(PCBA pcba)
    {
        var pcbaModel = new PCBAModel()
        {
           Uid = pcba.Uid,
           ManufacturerNumber = pcba.ManufacturerNumber,
           ItemNumber = pcba.ItemNumber,
           Software = pcba.Software,
           ProductionDateCode = pcba.ProductionDateCode,
           ConfigNo = pcba.ConfigNo
        };
        return pcbaModel;
    }
}