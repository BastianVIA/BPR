using AutoFixture;
using TestResult.Domain.Entities;

namespace TestResult.Tests.Util;

public static class EntityCreator
{
    private static Fixture _fixture = new();

    public static TestError CreateTestError(
        Guid? id = null,
        int? woNo = null,
        int? serialNo = null,
        string? tester = null,
        int? bay = null,
        int? errorCode = null,
        string? errorMessage = null,
        DateTime? timeOccured = null)
    {
        return new TestError(
            id ?? _fixture.Create<Guid>(),
            tester ?? _fixture.Create<string>(),
            bay ?? _fixture.Create<int>(),
            errorCode ?? _fixture.Create<int>(),
            errorMessage ?? _fixture.Create<string>(),
            timeOccured ?? _fixture.Create<DateTime>(),
            woNo ?? _fixture.Create<int>(),
            serialNo ?? _fixture.Create<int>());
    }
}