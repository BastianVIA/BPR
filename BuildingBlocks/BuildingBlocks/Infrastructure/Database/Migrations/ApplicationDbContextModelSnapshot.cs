﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BuildingBlocks.Infrastructure.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.ActuatorModel", b =>
                {
                    b.Property<int>("WorkOrderNumber")
                        .HasColumnType("int");

                    b.Property<int>("SerialNumber")
                        .HasColumnType("int");

                    b.Property<string>("PCBAUid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("WorkOrderNumber", "SerialNumber");

                    b.HasIndex("PCBAUid");

                    b.ToTable("Actuators");
                });

            modelBuilder.Entity("Infrastructure.PCBAModel", b =>
                {
                    b.Property<string>("Uid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ManufacturerNumber")
                        .HasColumnType("int");

                    b.HasKey("Uid");

                    b.ToTable("PCBAs");
                });

            modelBuilder.Entity("Infrastructure.ActuatorModel", b =>
                {
                    b.HasOne("Infrastructure.PCBAModel", "PCBA")
                        .WithMany()
                        .HasForeignKey("PCBAUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PCBA");
                });
#pragma warning restore 612, 618
        }
    }
}
