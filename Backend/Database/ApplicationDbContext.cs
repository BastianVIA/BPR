using Backend.Model;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public virtual DbSet<ActuatorModel> ActuatorModel { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActuatorModel>()
            .HasKey(a => new { a.WorkOrderNumber, a.SerialNumber });
        base.OnModelCreating(modelBuilder);
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}