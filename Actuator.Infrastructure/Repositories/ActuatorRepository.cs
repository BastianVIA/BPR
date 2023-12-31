﻿using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database;
using BuildingBlocks.Infrastructure.Database.Models;
using Domain.Entities;
using Domain.RepositoryInterfaces;
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
        var article = await getArticle(actuator.ArticleNumber, actuator.ArticleName);
        var actuatorModel = FromDomain(actuator);
        actuatorModel.PCBA = pcba;
        actuatorModel.Article = article;
        await AddAsync(actuatorModel, actuator.GetDomainEvents());
    }

    public async Task<Actuator> GetActuator(CompositeActuatorId id)
    {
        var actuatorModel = await Query()
            .Include(model => model.PCBA)
            .Include(model => model.Article)
            .FirstOrDefaultAsync(a =>
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
        var actuatorFromDb = await Query()
            .Include(model => model.PCBA)
            .Include(model => model.Article)
            .FirstAsync(a =>
                a.WorkOrderNumber == actuator.Id.WorkOrderNumber && a.SerialNumber == actuator.Id.SerialNumber);
        var pcba = await getPcbaModel(actuator.PCBA.Uid);
        actuatorFromDb.PCBA = pcba;
        var article = await getArticle(actuator.ArticleNumber, actuator.ArticleName);
        actuatorFromDb.Article = article;
        await UpdateAsync(actuatorFromDb, actuator.GetDomainEvents());
    }

    public async Task<List<Actuator>> GetActuatorsWithFilter(int? woNo, int? serialNo, string? pcbaUid,
        string? pcbaItemNumber, int? pcbaManufacturerNumber, int? pcbaProductionDateCode, string? communicationProtocol,
        string? articleNumber, string? configNo, string? software, DateTime? startDate, DateTime? endDate)
    {
        var queryBuilder = Query()
            .Include(model => model.PCBA)
            .Include(model => model.Article)
            .AsQueryable();

        if (woNo != null)
        {
            queryBuilder = queryBuilder.Where(model => model.WorkOrderNumber == woNo);
        }

        if (serialNo != null)
        {
            queryBuilder = queryBuilder.Where(model => model.SerialNumber == serialNo);
        }

        if (pcbaUid != null)
        {
            queryBuilder = queryBuilder.Where(model => model.PCBA.Uid == pcbaUid);
        }

        if (pcbaItemNumber != null)
        {
            queryBuilder = queryBuilder.Where(model => model.PCBA.ItemNumber == pcbaItemNumber);
        }

        if (pcbaManufacturerNumber != null)
        {
            queryBuilder = queryBuilder.Where(model => model.PCBA.ManufacturerNumber == pcbaManufacturerNumber);
        }

        if (pcbaProductionDateCode != null)
        {
            queryBuilder = queryBuilder.Where(model => model.PCBA.ProductionDateCode == pcbaProductionDateCode);
        }

        if (communicationProtocol != null)
        {
            queryBuilder = queryBuilder.Where(model => model.CommunicationProtocol == communicationProtocol);
        }

        if (articleNumber != null)
        {
            queryBuilder = queryBuilder.Where(model => model.ArticleNumber == articleNumber);
        }

        if (configNo != null)
        {
            queryBuilder = queryBuilder.Where(model => model.PCBA.ConfigNo == configNo);
        }

        if (software != null)
        {
            queryBuilder = queryBuilder.Where(model => model.PCBA.Software == software);
        }

        if (startDate != null)
        {
            queryBuilder = queryBuilder.Where(model => model.CreatedTime > startDate);
        }

        if (endDate != null)
        {
            queryBuilder = queryBuilder.Where(model => model.CreatedTime < endDate);
        }

        var actuatorModels = await queryBuilder.ToListAsync();
        return ToDomain(actuatorModels);
    }

    public async Task<List<Actuator>> GetActuatorsFromPCBAAsync(string requestUid, int? requestManufacturerNo = null)
    {
        var query = Query()
            .Include(model => model.PCBA)
            .Include(model => model.Article)
            .Where(model => model.PCBA.Uid == requestUid);
        if (requestManufacturerNo != null)
        {
            query = query.Where(model => model.PCBA.ManufacturerNumber == requestManufacturerNo);
        }

        var result = await query.ToListAsync();
        return ToDomain(result);
    }

    public async Task<int> GetTotalActuatorAmount()
    {
        return Query().Count();
    }

    private Actuator ToDomain(ActuatorModel actuatorModel)
    {
        var actuatorId = CompositeActuatorId.From(actuatorModel.WorkOrderNumber, actuatorModel.SerialNumber);
        var pcba = new PCBA(
            uid: actuatorModel.PCBA.Uid,
            manufacturerNo: actuatorModel.PCBA.ManufacturerNumber,
            itemNumber: actuatorModel.PCBA.ItemNumber,
            software: actuatorModel.PCBA.Software,
            productionDateCode: actuatorModel.PCBA.ProductionDateCode,
            configNo: actuatorModel.PCBA.ConfigNo);
        return new Actuator(actuatorId, pcba, actuatorModel.ArticleNumber, actuatorModel.Article.ArticleName,
            actuatorModel.CommunicationProtocol, actuatorModel.CreatedTime);
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

    private ActuatorModel FromDomain(Actuator actuator)
    {
        var pcbaModel = new PCBAModel()
        {
            Uid = actuator.PCBA.Uid,
            ManufacturerNumber = actuator.PCBA.ManufacturerNumber
        };
        var articleModel = new ArticleModel()
        {
            ArticleNumber = actuator.ArticleNumber,
            ArticleName = actuator.ArticleName
        };
        var actuatorModel = new ActuatorModel()
        {
            WorkOrderNumber = actuator.Id.WorkOrderNumber,
            SerialNumber = actuator.Id.SerialNumber,
            PCBA = pcbaModel,
            ArticleNumber = actuator.ArticleNumber,
            CommunicationProtocol = actuator.CommunicationProtocol,
            CreatedTime = actuator.CreatedTime,
            Article = articleModel
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

    private async Task<ArticleModel> getArticle(string articleNumber, string? articleName)
    {
        ArticleModel? articleModel;
        try
        {
            articleModel = QueryOtherLocal<ArticleModel>()
                .FirstOrDefault(a => a.ArticleNumber == articleNumber);
            if (articleModel == null)
            {
                articleModel = await QueryOther<ArticleModel>().FirstAsync(a => a.ArticleNumber == articleNumber);
            }
        }
        catch (Exception e)
        {
            if (articleName is null)
            {
                throw new ArgumentException($"Could not find message for article with number {articleNumber}");
            }

            articleModel = new ArticleModel
            {
                ArticleNumber = articleNumber,
                ArticleName = articleName
            };
        }


        return articleModel;
    }
}