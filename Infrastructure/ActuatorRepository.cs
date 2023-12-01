using System.Runtime.CompilerServices;
using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ActuatorRepository : BaseRepository<ActuatorModel>, IActuatorRepository
{
    public ActuatorRepository(ApplicationDbContext dbContext, IScheduler scheduler) : base(dbContext, scheduler)
    {
    }

    public async Task CreateActuator(Actuator actuator)
    {
        var pcba = await getPcbaModel(actuator.PCBA.Uid);
        var actuatorModel = FromDomain(actuator);
        actuatorModel.PCBA = pcba;
        await AddAsync(actuatorModel, actuator.GetDomainEvents());
    }

    public async Task<Actuator> GetActuator(CompositeActuatorId id)
    {
        var actuatorModel = await Query().Include(model => model.PCBA).FirstOrDefaultAsync(a =>
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
        var actuatorFromDb = await Query().Include(model => model.PCBA).FirstAsync(a =>
            a.WorkOrderNumber == actuator.Id.WorkOrderNumber && a.SerialNumber == actuator.Id.SerialNumber);
        var pcba = await getPcbaModel(actuator.PCBA.Uid);

        actuatorFromDb.PCBA = pcba;
        await UpdateAsync(actuatorFromDb, actuator.GetDomainEvents());
    }

    public async Task<List<Actuator>> GetActuatorsWithFilter(int? requestItemNumber, int? requestManufacturerNumber,
        int? requestProductionDateCode)
    {
        var actuatorFromDb = Query().Include(model => model.PCBA).AsQueryable();
        // if (requestItemNumber != null)
        // {
        //     actuatorFromDb = actuatorFromDb.Where(model => model.PCBA.ItemNo == requestItemNumber);
        // }
        if (requestManufacturerNumber != null)
        {
            actuatorFromDb = actuatorFromDb.Where(model => model.PCBA.ManufacturerNumber == requestManufacturerNumber);
        }

        // if (requestProductionDateCode != null)
        // {
        //     actuatorFromDb = actuatorFromDb.Where(model => model.PCBA.productionDateCode == requestProductionDateCode);
        // }
        var actuatorModel = await actuatorFromDb.ToListAsync();
        return ToDomain(actuatorModel);
    }

    private List<Actuator> ToDomain(List<ActuatorModel> actuatorModels)
    {
        List<Actuator> domainActuators = new();
        foreach (var actuatorModel in actuatorModels)
        {
            domainActuators.Add(ToDomain(actuatorModel));
        }

        return domainActuators;
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

    private async Task<PCBAModel> getPcbaModel(string uid)
    {
        var pcba = QueryOtherLocal<PCBAModel>().FirstOrDefault(m => m.Uid == uid);
        if (pcba == null)
        {
            pcba = await QueryOther<PCBAModel>().FirstAsync(m => m.Uid == uid);
        }

        return pcba;
    }
}