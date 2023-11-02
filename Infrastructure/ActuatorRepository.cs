using BuildingBlocks.Exceptions;
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
        var actuatorModel = FromDomain(actuator);
        _dbContext.Entry(actuatorModel.PCBA).State = EntityState.Unchanged;
        await Add(actuatorModel);
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

    public async Task UpdateActuator(Actuator actuator)
    {
        await Update(FromDomain(actuator));
    }

    private Actuator ToDomain(ActuatorModel actuatorModel)
    {
        var actuatorId = CompositeActuatorId.From(actuatorModel.WorkOrderNumber, actuatorModel.SerialNumber);
        var pcba = new PCBA(uid: actuatorModel.PCBA.Uid, manufacturerNo: actuatorModel.PCBA.ManufacturerNumber);
        return new Actuator(actuatorId, pcba);
    }

    private ActuatorModel FromDomain(Actuator actuator)
    {
        var pcbaModel = new PCBAModel()
        {
            Uid = actuator.PCBA.Uid,
            ManufacturerNumber = actuator.PCBA.ManufacturerNumber
        };
        
        var actuatorModel = new ActuatorModel()
        {
            WorkOrderNumber = actuator.Id.WorkOrderNumber,
            SerialNumber = actuator.Id.SerialNumber,
            PCBA = pcbaModel
        };
        return actuatorModel;
    }
}