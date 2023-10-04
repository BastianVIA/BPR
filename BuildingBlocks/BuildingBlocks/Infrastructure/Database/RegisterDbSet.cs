using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure.Database;

public static class RegisterDbSet
{
    public static IServiceCollection AddEntityDbSet<T>(this IServiceCollection services, Action<EntityTypeBuilder<T>> configure)
        where T : class
    {
        services.AddScoped<DbSet<T>>(provider =>
        {
            var dbContext = provider.GetRequiredService<ApplicationDbContext>();
            var modelBuilder = new ModelBuilder(new ConventionSet());

            // Perform the configuration for the entity
            modelBuilder.Entity<T>(configure); // Here, configure is Action<EntityTypeBuilder<T>>

            return dbContext.Set<T>();
        });

        return services;
    }
}