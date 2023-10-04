using Infrastructure;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<ActuatorModel> Actuators { get; set; } 
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActuatorModel>().HasKey(a => new { a.WorkOrderNumber, a.SerialNumber });
    }
}