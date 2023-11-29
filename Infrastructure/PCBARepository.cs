using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database;
using Domain.Entities;
using Domain.Repositories;
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
        try
        {
            await UpdateAsync(FromDomain(pcba), pcba.GetDomainEvents());
        }
        catch (Exception e)
        {
            var localPcba = QueryOtherLocal<PCBAModel>().FirstOrDefault(m => m.Uid == pcba.Uid);
            if (localPcba == null)
            {
                localPcba = await QueryOther<PCBAModel>().FirstAsync(m => m.Uid == pcba.Uid);
            }
            localPcba.ManufacturerNumber = pcba.ManufacturerNumber;
            localPcba.ItemNumber = pcba.ItemNumber;
            localPcba.Software = pcba.Software;
            localPcba.ProductionDateCode = pcba.ProductionDateCode;
            await UpdateAsync(localPcba, pcba.GetDomainEvents());
        }
        
    }

    private PCBA ToDomain(PCBAModel pcbaModel)
    {
        return new PCBA(pcbaModel.Uid, pcbaModel.ManufacturerNumber, pcbaModel.ItemNumber, pcbaModel.Software, pcbaModel.ProductionDateCode);
    }
    
    private PCBAModel FromDomain(PCBA pcba)
    {
        var pcbaModel = new PCBAModel()
        {
           Uid = pcba.Uid,
           ManufacturerNumber = pcba.ManufacturerNumber,
           ItemNumber = pcba.ItemNumber,
           Software = pcba.Software,
           ProductionDateCode = pcba.ProductionDateCode
        };
        return pcbaModel;
    }
}