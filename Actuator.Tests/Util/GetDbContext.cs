using Microsoft.EntityFrameworkCore;

namespace Actuator.Tests.Util;

public static class GetDbContext
{
    public static ApplicationDbContext GetContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        //var dbContext = Substitute.For<ApplicationDbContext>(options);
        var db = new ApplicationDbContext(options);
        
        return db;
    }
}