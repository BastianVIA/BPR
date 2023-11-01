using BuildingBlocks.Infrastructure.Database;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ActuatorRepository : BaseRepository<ActuatorModel>, IActuatorRepository
{
    public ActuatorRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task CreateActuator(Actuator actuator)
    {
        await Add(FromDomain(actuator));
    }

    public async Task<Actuator> GetActuator(CompositeActuatorId id)
    {
        var actuatorModel = await Query().FirstOrDefaultAsync(a =>
            a.WorkOrderNumber == id.WorkOrderNumber && a.SerialNumber == id.SerialNumber);
        if (actuatorModel == null)
        {
            throw new KeyNotFoundException(
                $"Could not find Actuator with WorkOrderNumber: {id.WorkOrderNumber} and SerialNumber: {id.SerialNumber}");
        }

        return ToDomain(actuatorModel);
    }

    private Actuator ToDomain(ActuatorModel actuatorModel)
    {
        var actuatorId = CompositeActuatorId.From(actuatorModel.WorkOrderNumber, actuatorModel.SerialNumber);
        return new Actuator(actuatorId, actuatorModel.PCBAUid);
    }

    private ActuatorModel FromDomain(Actuator actuator)
    {
        var actuatorModel = new ActuatorModel()
        {
            WorkOrderNumber = actuator.Id.WorkOrderNumber,
            SerialNumber = actuator.Id.SerialNumber,
            PCBAUid = actuator.PCBAUid
        };
        return actuatorModel;
    }
}