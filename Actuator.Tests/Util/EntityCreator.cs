using AutoFixture;
using Domain.Entities;

namespace Backend.Tests.Util;

public static class EntityCreator
{
    private static Fixture _fixture = new();
    public static Domain.Entities.Actuator CreateActuator(int? woNo = null, int? serialNo = null, string? pcbaUid = null, int? manuNumber = null, string? itemNumber = null, string? software = null, int? prodDateCode = null, string? configNo = null, string? artNo = null, string? artName = null, string? commProtocol = null, DateTime? createdTime = null)
    {
        return Domain.Entities.Actuator.Create(
            CompositeActuatorId.From(
                woNo ?? _fixture.Create<int>(),
                serialNo ?? _fixture.Create<int>()),
            PCBA.Create(
                pcbaUid ?? _fixture.Create<string>(),
                manuNumber ?? _fixture.Create<int>(),
                itemNumber ?? _fixture.Create<string>(),
                software ?? _fixture.Create<string>(),
                prodDateCode ?? _fixture.Create<int>(),
                configNo ?? _fixture.Create<string>()),
            artNo ?? _fixture.Create<string>(),
            artName ?? _fixture.Create<string>(),
            commProtocol ?? _fixture.Create<string>(),
            createdTime ?? _fixture.Create<DateTime>());
    }
}