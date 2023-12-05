using BuildingBlocks.Infrastructure.Database.Models;
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
        
        modelBuilder.Entity<ActuatorPCBAHistoryModel>()
            .HasOne(a => a.ActuatorModel)
            .WithMany()
            .HasForeignKey(a => new { a.WorkOrderNumber, a.SerialNumber })
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ActuatorPCBAHistoryModel>()
            .HasOne(a => a.PCBA)
            .WithMany()
            .HasForeignKey(a => a.PCBAUid)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<ActuatorPCBAHistoryModel>().HasKey(a => new
            { a.WorkOrderNumber, a.SerialNumber, a.PCBAUid, a.RemovalTime }).IsClustered(false);
    }
}