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

    public async Task<Actuator> GetActuator(CompositeActuatorId actuatorId)
    {
        var actuatorModel = await Query().FirstAsync(a =>
            a.WorkOrderNumber == actuatorId.WorkOrderNumber && a.SerialNumber == actuatorId.SerialNumber);

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
            WorkOrderNumber = actuator.ActuatorId.WorkOrderNumber,
            SerialNumber = actuator.ActuatorId.SerialNumber,
            PCBAUid = actuator.PCBAUId
        };
        return actuatorModel;
    }
}