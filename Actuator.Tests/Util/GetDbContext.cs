using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Actuator.Tests.Util;

public static class GetDbContext
{
    public static ApplicationDbContext GetContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        return new ApplicationDbContext(options);
    }
}