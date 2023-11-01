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

    public async Task<PCBA> GetPCBA(string id)
    {
        var pcbaModel = await Query().FirstAsync(p => p.Uid == id);

        return ToDomain(pcbaModel);
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