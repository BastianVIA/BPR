using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database;
using BuildingBlocks.Infrastructure.Database.Models;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ActuatorPCBAHistoryRepository : BaseRepository<ActuatorPCBAHistoryModel>, IActuatorPCBAHistoryRepository
{
    public ActuatorPCBAHistoryRepository(ApplicationDbContext dbContext, IScheduler scheduler) : base(dbContext, scheduler)
    {
    }

    public async Task PCBARemoved(ActuatorPCBAChange change)
    {
        var newModel = new ActuatorPCBAHistoryModel
        {
            WorkOrderNumber = change.WorkOrderNumber,
            SerialNumber = change.SerialNumber,
            PCBAUid = change.OldPCBAUid,
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