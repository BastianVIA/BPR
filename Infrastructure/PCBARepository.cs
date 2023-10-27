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
        await Add(FromDomain(pcba));
    }

    public async Task<PCBA> GetPCBA(int id)
    {
        var pcbaModel = await Query().FirstAsync(p => p.PCBAUid == id);

        return ToDomain(pcbaModel);
    }
    
    private PCBA ToDomain(PCBAModel pcbaModel)
    {
        return new PCBA(pcbaModel.PCBAUid, pcbaModel.ManufacturerNumber);
    }
    
    private PCBAModel FromDomain(PCBA pcba)
    {
        var pcbaModel = new PCBAModel()
        {
           PCBAUid = pcba.PCBAUid,
           ManufacturerNumber = pcba.ManufacturerNumber
        };
        return pcbaModel;
    }
}