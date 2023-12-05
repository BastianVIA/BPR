using Infrastructure;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<ActuatorModel> Actuators { get; set; } 
    public DbSet<PCBAModel> PCBAs { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActuatorModel>().HasKey(a => new { a.WorkOrderNumber, a.SerialNumber });
        modelBuilder.Entity<PCBAModel>().HasKey(p => p.Uid);
        modelBuilder.Entity<InboxMessageModel>().HasKey(i => i.Id);
        
        modelBuilder.Entity<TestErrorModel>().HasKey(t => t.Id);
        modelBuilder.Entity<TestResultModel>().HasKey(t => t.Id);
        modelBuilder.Entity<TestResultModel>()
            .HasMany(t => t.TestErrors)
            .WithOne()
            .HasForeignKey(t => t.TestResultId);
    }
}