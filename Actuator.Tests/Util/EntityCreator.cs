using AutoFixture;
using BuildingBlocks.Infrastructure.Database.Models;
using Domain.Entities;

namespace Actuator.Tests.Util;

public static class EntityCreator
{
    private static Fixture _fixture = new();

    public static Domain.Entities.Actuator CreateActuator(int? woNo = null, int? serialNo = null,
        string? pcbaUid = null, int? manufacturerNo = null, string? itemNumber = null, string? software = null,
        int? prodDateCode = null, string? configNo = null, string? artNo = null, string? artName = null,
        string? commProtocol = null, DateTime? createdTime = null)
    {
        return Domain.Entities.Actuator.Create(
            CompositeActuatorId.From(
                woNo ?? _fixture.Create<int>(),
                serialNo ?? _fixture.Create<int>()),
            PCBA.Create(
                pcbaUid ?? _fixture.Create<string>(),
                manufacturerNo ?? _fixture.Create<int>(),
                itemNumber ?? _fixture.Create<string>(),
                software ?? _fixture.Create<string>(),
                prodDateCode ?? _fixture.Create<int>(),
                configNo ?? _fixture.Create<string>()),
            artNo ?? _fixture.Create<string>(),
            artName ?? _fixture.Create<string>(),
            commProtocol ?? _fixture.Create<string>(),
            createdTime ?? _fixture.Create<DateTime>());
    }

    public static Domain.Entities.PCBA CreatePCBA(int prodDateCode, int manuNo, string? software = null,
        string? itemNumber = null,
        string? configNo = null, string? uid = null)
    {
        return PCBA.Create(uid, manuNo, itemNumber, software, prodDateCode, configNo);
    }

    public static ActuatorModel CreateActuatorModel(int? woNo = null, int? serialNo = null, string? comProto = null,
        string? artNo = null, DateTime? createdTime = null, PCBAModel? pcbaModel = null, ArticleModel? article = null)
    {
        return new ActuatorModel()
        {
            ArticleNumber = artNo ?? _fixture.Create<string>(),
            Article = article ?? CreateArticleModel(),
            CommunicationProtocol = comProto ?? _fixture.Create<string>(),
            CreatedTime = createdTime ?? _fixture.Create<DateTime>(),
            SerialNumber = serialNo ?? _fixture.Create<int>(),
            WorkOrderNumber = woNo ?? _fixture.Create<int>(),
            PCBA = pcbaModel ?? CreatePCBAModel()
        };
    }

    public static PCBAModel CreatePCBAModel(string? software = null, string? itemNumber = null, int? manuNo = null,
        string? configNo = null, int? prodDateCode = null, string? uid = null)
    {
        return new PCBAModel()
        {
            ConfigNo = configNo ?? _fixture.Create<string>(),
            ItemNumber = itemNumber ?? _fixture.Create<string>(),
            ManufacturerNumber = manuNo ?? _fixture.Create<int>(),
            Uid = uid ?? _fixture.Create<string>(),
            Software = software ?? _fixture.Create<string>(),
            ProductionDateCode = prodDateCode ?? _fixture.Create<int>()
        };
    }

    public static ArticleModel CreateArticleModel(string? artNo = null, string? artName = null)
    {
        return new ArticleModel()
        {
            ArticleNumber = artNo ?? _fixture.Create<string>(),
            ArticleName = artName ?? _fixture.Create<string>()
        };
    }
}