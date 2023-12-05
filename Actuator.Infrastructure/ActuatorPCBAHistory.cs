using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database;
using BuildingBlocks.Infrastructure.Database.Models;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ActuatorPCBAHistory : BaseRepository<ActuatorPCBAHistoryModel>, IActuatorPCBAHistory
{
    public ActuatorPCBAHistory(ApplicationDbContext dbContext, IScheduler scheduler) : base(dbContext, scheduler)
    {
    }

    public async Task PCBARemoved(ActuatorPCBAChange change)
    {
        var actuator = await QueryOther<ActuatorModel>().AsNoTracking()
            .FirstAsync(model =>
                model.WorkOrderNumber == change.WorkOrderNumber && model.SerialNumber == change.SerialNumber);
        var pcba = await QueryOther<PCBAModel>().AsNoTracking().FirstAsync(model => model.Uid == change.OldPCBAUid);

        var newModel = new ActuatorPCBAHistoryModel
        {
            ActuatorModel = actuator,
            PCBA = pcba,
            RemovalTime = change.RemovalTime
        };
        await AddAsync(newModel, new List<IDomainEvent>());
    }

    public async Task<List<ActuatorPCBAChange>> GetPCBAChangesForActuator(int woNo, int serialNo)
    {
        var allChanges = await Query().Where(model => model.WorkOrderNumber == woNo && model.SerialNumber == serialNo)
            .ToListAsync();
        return ToDomain(allChanges);
    }


    private List<ActuatorPCBAChange> ToDomain(List<ActuatorPCBAHistoryModel> changesAsModel)
    {
        List<ActuatorPCBAChange> domainChanges = new();
        foreach (var model in changesAsModel)
        {
            domainChanges.Add(ToDomain(model));
        }

        return domainChanges;
    }

    private ActuatorPCBAChange ToDomain(ActuatorPCBAHistoryModel changeAsModel)
    {
        return new ActuatorPCBAChange(changeAsModel.WorkOrderNumber, changeAsModel.SerialNumber, changeAsModel.PCBAUid,
            changeAsModel.RemovalTime);
    }
}