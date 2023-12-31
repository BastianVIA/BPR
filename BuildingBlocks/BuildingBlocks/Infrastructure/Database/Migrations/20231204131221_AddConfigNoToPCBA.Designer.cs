﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BuildingBlocks.Infrastructure.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231204131221_AddConfigNoToPCBA")]
    partial class AddConfigNoToPCBA
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("Infrastructure.InboxMessageModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FailedAttempts")
                        .HasColumnType("int");

                    b.Property<string>("FailureReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IntegrationEventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MessageType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Payload")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("InboxMessageModel");
                });

            modelBuilder.Entity("Infrastructure.PCBAModel", b =>
                {
                    b.Property<string>("Uid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConfigNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManufacturerNumber")
                        .HasColumnType("int");

                    b.Property<int>("ProductionDateCode")
                        .HasColumnType("int");

                    b.Property<string>("Software")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
