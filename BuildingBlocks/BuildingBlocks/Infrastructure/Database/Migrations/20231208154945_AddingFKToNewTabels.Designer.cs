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
    [Migration("20231208154945_AddingFKToNewTabels")]
    partial class AddingFKToNewTabels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BuildingBlocks.Infrastructure.Database.Models.ActuatorModel", b =>
                {
                    b.Property<int>("WorkOrderNumber")
                        .HasColumnType("int");

                    b.Property<int>("SerialNumber")
                        .HasColumnType("int");

                    b.Property<string>("ArticleNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CommunicationProtocol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PCBAUid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("WorkOrderNumber", "SerialNumber");

                    b.HasIndex("ArticleNumber");

                    b.HasIndex("PCBAUid");

                    b.ToTable("Actuators");
                });

            modelBuilder.Entity("BuildingBlocks.Infrastructure.Database.Models.ActuatorPCBAHistoryModel", b =>
                {
                    b.Property<int>("WorkOrderNumber")
                        .HasColumnType("int");

                    b.Property<int>("SerialNumber")
                        .HasColumnType("int");

                    b.Property<string>("PCBAUid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RemovalTime")
                        .HasColumnType("datetime2");

                    b.HasKey("WorkOrderNumber", "SerialNumber", "PCBAUid", "RemovalTime");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("WorkOrderNumber", "SerialNumber", "PCBAUid", "RemovalTime"), false);

                    b.HasIndex("PCBAUid");

                    b.ToTable("ActuatorPCBAHistoryModel");
                });

            modelBuilder.Entity("BuildingBlocks.Infrastructure.Database.Models.ArticleModel", b =>
                {
                    b.Property<string>("ArticleNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArticleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArticleNumber");

                    b.ToTable("ArticleModel");
                });

            modelBuilder.Entity("BuildingBlocks.Infrastructure.Database.Models.FailingInboxMessageModel", b =>
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

                    b.Property<bool>("IsFailing")
                        .HasColumnType("bit");

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

                    b.ToTable("FailingInboxMessageModel");
                });

            modelBuilder.Entity("BuildingBlocks.Infrastructure.Database.Models.InboxMessageModel", b =>
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

                    b.Property<bool>("IsFailing")
                        .HasColumnType("bit");

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

            modelBuilder.Entity("BuildingBlocks.Infrastructure.Database.Models.PCBAModel", b =>
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

            modelBuilder.Entity("BuildingBlocks.Infrastructure.Database.Models.TestErrorCodeModel", b =>
                {
                    b.Property<int>("ErrorCode")
                        .HasColumnType("int");

                    b.Property<string>("ErrorMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ErrorCode");

                    b.ToTable("TestErrorCodeModel");
                });

            modelBuilder.Entity("BuildingBlocks.Infrastructure.Database.Models.TestErrorModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Bay")
                        .HasColumnType("int");

                    b.Property<int>("ErrorCode")
                        .HasColumnType("int");

                    b.Property<Guid>("TestResultId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Tester")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeOccured")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ErrorCode");

                    b.HasIndex("TestResultId");

                    b.ToTable("TestErrorModel");
                });

            modelBuilder.Entity("BuildingBlocks.Infrastructure.Database.Models.TestResultModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Bay")
                        .HasColumnType("int");

                    b.Property<string>("MaxBuslinkPosition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaxServoPosition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MinBuslinkPosition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MinServoPosition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SerialNumber")
                        .HasColumnType("int");

                    b.Property<string>("ServoStroke")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tester")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeOccured")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkOrderNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TestResultModel");
                });

            modelBuilder.Entity("BuildingBlocks.Infrastructure.Database.Models.ActuatorModel", b =>
                {
                    b.HasOne("BuildingBlocks.Infrastructure.Database.Models.ArticleModel", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleNumber")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BuildingBlocks.Infrastructure.Database.Models.PCBAModel", "PCBA")
                        .WithMany()
                        .HasForeignKey("PCBAUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("PCBA");
                });

            modelBuilder.Entity("BuildingBlocks.Infrastructure.Database.Models.ActuatorPCBAHistoryModel", b =>
                {
                    b.HasOne("BuildingBlocks.Infrastructure.Database.Models.PCBAModel", "PCBA")
                        .WithMany()
                        .HasForeignKey("PCBAUid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BuildingBlocks.Infrastructure.Database.Models.ActuatorModel", "ActuatorModel")
                        .WithMany()
                        .HasForeignKey("WorkOrderNumber", "SerialNumber")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ActuatorModel");

                    b.Navigation("PCBA");
                });

            modelBuilder.Entity("BuildingBlocks.Infrastructure.Database.Models.TestErrorModel", b =>
                {
                    b.HasOne("BuildingBlocks.Infrastructure.Database.Models.TestErrorCodeModel", "ErrorCodeModel")
                        .WithMany()
                        .HasForeignKey("ErrorCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BuildingBlocks.Infrastructure.Database.Models.TestResultModel", null)
                        .WithMany("TestErrors")
                        .HasForeignKey("TestResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ErrorCodeModel");
                });

            modelBuilder.Entity("BuildingBlocks.Infrastructure.Database.Models.TestResultModel", b =>
                {
                    b.Navigation("TestErrors");
                });
#pragma warning restore 612, 618
        }
    }
}
