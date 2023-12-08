using BuildingBlocks.Infrastructure.Database.Models;
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
        modelBuilder.Entity<FailingInboxMessageModel>().HasKey(i => i.Id);
        modelBuilder.Entity<ArticleModel>().HasKey(a => a.ArticleNumber);
        modelBuilder.Entity<TestErrorCodeModel>().Property(e => e.ErrorCode).ValueGeneratedNever();
        modelBuilder.Entity<TestErrorCodeModel>().HasKey(e => e.ErrorCode);
        
        BuildActuatorModel(modelBuilder);
        BuildActuatorPCBAHistoryModel(modelBuilder);
        BuildTestModel(modelBuilder);
    }

    private void BuildActuatorModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActuatorModel>()
            .HasOne(a => a.Article)
            .WithMany()
            .HasForeignKey(a => a.ArticleNumber)
            .OnDelete(DeleteBehavior.Restrict);

    }

    private static void BuildActuatorPCBAHistoryModel(ModelBuilder modelBuilder)
    {
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

    private static void BuildTestModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestErrorModel>().HasKey(t => t.Id);
        
        modelBuilder.Entity<TestErrorModel>()
            .HasOne(e => e.ErrorCodeModel)
            .WithMany()
            .HasForeignKey(e => e.ErrorCode)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<TestResultModel>().HasKey(t => t.Id);
        modelBuilder.Entity<TestResultModel>()
            .HasMany(t => t.TestErrors)
            .WithOne()
            .HasForeignKey(t => t.TestResultId);
    }
}