﻿using Actuator.Tests.Util;
using AutoFixture;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Infrastructure;
using BuildingBlocks.Infrastructure.Database.Migrations;
using BuildingBlocks.Infrastructure.Database.Models;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NSubstitute;
using Xunit.Abstractions;

namespace Actuator.Tests.Infrastructure;

public class ActuatorRepositoryTests
{
    private Fixture _fixture = new();
    private ApplicationDbContext _dbContext = GetDbContext.GetContext();
    private ActuatorRepository _repository;
    private IScheduler _scheduler = Substitute.For<IScheduler>();

    public ActuatorRepositoryTests()
    {
        _repository = new ActuatorRepository(_dbContext, _scheduler);
    }

    [Fact]
    public async Task Create_Should_AddToDatabase()
    {
        // Arrange
        var countBefore = _dbContext.Actuators.Count();
        var actuator = EntityCreator.CreateActuator();
        _dbContext.PCBAs.Add(EntityCreator.CreatePCBAModel(uid: actuator.PCBA.Uid));

        await _dbContext.SaveChangesAsync();

        // Act
        await _repository.CreateActuator(actuator);

        // Saving changes, so the count of actuators can be fetched
        await _dbContext.SaveChangesAsync();

        // Assert
        var countAfter = _dbContext.Actuators.Count();
        Assert.Equal(countBefore + 1, countAfter);
    }

    [Fact]
    public async Task Create_AddsRightObject()
    {
        var actuator = EntityCreator.CreateActuator();
        //var model = Domain.Entities.Actuator.Create(actuator.Id, actuator.PCBA, actuator.ArticleNumber, actuator.ArticleName, actuator.CommunicationProtocol, actuator.CreatedTime);
        _dbContext.PCBAs.Add(EntityCreator.CreatePCBAModel(uid: actuator.PCBA.Uid));
        await _dbContext.SaveChangesAsync();

        await _repository.CreateActuator(actuator);
        await _dbContext.SaveChangesAsync();

        var added = _dbContext.Actuators.First();

        Assert.Equal(actuator.Id.SerialNumber, added.SerialNumber);
        Assert.Equal(actuator.Id.WorkOrderNumber, added.WorkOrderNumber);
        Assert.Equal(actuator.PCBA.Uid, added.PCBA.Uid);
    }

    [Fact]
    public async Task Create_ShouldNotAddActuator_WhenAddFails()
    {
        // Arrange
        var actuator = EntityCreator.CreateActuator();
        _dbContext.Actuators.Add(EntityCreator.CreateActuatorModel(
            woNo: actuator.Id.WorkOrderNumber,
            serialNo: actuator.Id.SerialNumber
        ));

        await _dbContext.SaveChangesAsync();

        // Act/Assert
        await Assert.ThrowsAnyAsync<Exception>(() => _repository.CreateActuator(actuator));
    }

    [Fact]
    public async Task GetActuator_ThrowsKeyNotFoundException_WhenActuatorNotFound()
    {
        var compId = _fixture.Create<CompositeActuatorId>();

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _repository.GetActuator(compId));
    }

    [Fact]
    public async Task GetActuator_ReturnsActuator_WhenActuatorFound()
    {
        var expected = _fixture.Create<Domain.Entities.Actuator>();
        await SetupActuator(expected);

        var result = await _repository.GetActuator(expected.Id);

        Assert.NotNull(result);
        Assert.IsType<Domain.Entities.Actuator>(result);
    }

    [Fact]
    public async Task GetActuator_ReturnsRightActuator_WhenActuatorFound()
    {
        var expected = _fixture.Create<Domain.Entities.Actuator>();
        await SetupActuator(expected);

        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetActuator(expected.Id);

        Assert.NotNull(result);
        await AssertActuatorEquals(expected, result);
    }

    [Fact]
    public async Task GetActuatorFromPCBA_ReturnsListWithRightActuator_WhenActuatorsFound()
    {
        var noOfActuatorsToCreate = 3;
        var expected = _fixture.CreateMany<Domain.Entities.Actuator>(noOfActuatorsToCreate).ToList();
        await SetupMultipleActuators(expected);

        await _dbContext.SaveChangesAsync();

        var result =
            await _repository.GetActuatorsFromPCBAAsync(expected[0].PCBA.Uid, expected[0].PCBA.ManufacturerNumber);

        Assert.NotNull(result);

        await AssertActuatorEquals(expected[0], result[0]);
    }

    [Fact]
    public async Task GetActuatorFromPCBA_ReturnsEmptyList_WhenNoActuatorsFound()
    {
        var noOfActuatorsToCreate = 0;
        var expected = _fixture.CreateMany<Domain.Entities.Actuator>(noOfActuatorsToCreate).ToList();
        await SetupMultipleActuators(expected);

        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetActuatorsFromPCBAAsync("112233");

        Assert.NotNull(result);
        Assert.Empty(result);
    }
    
    [Fact]
    public async Task Update_ShouldChangeExistingActuator_WhenUpdating()
    {
        var init = CreateActuator();

        await SetupActuator(init);
        var beforeCount = _dbContext.Actuators.Count();
        var newActuator = Domain.Entities.Actuator.Create(init.Id, _fixture.Create<PCBA>(), init.ArticleNumber,
            init.ArticleName, init.CommunicationProtocol, init.CreatedTime);

        await SetupPCBA(newActuator.PCBA);
        await _repository.UpdateActuator(newActuator);
        var result = _dbContext.Actuators.First();

        Assert.NotNull(result);
        Assert.Equal(beforeCount, _dbContext.Actuators.Count());
    }

    [Fact]
    public async Task Update_ShouldThrowException_WhenUpdatingFails()
    {
        var actuatorNotInDb = CreateActuator();
        await Assert.ThrowsAnyAsync<Exception>(() => _repository.UpdateActuator(actuatorNotInDb));
    }

    private async Task SetupMultipleActuators(List<Domain.Entities.Actuator> actuators)
    {
        foreach (var actuator in actuators)
        {
            await SetupActuator(actuator);
        }
    }

    private async Task SetupActuator(Domain.Entities.Actuator actuator)
    {
        var pcba = EntityCreator.CreatePCBAModel(
            uid: actuator.PCBA.Uid,
            manuNo: actuator.PCBA.ManufacturerNumber,
            software: actuator.PCBA.Software,
            itemNumber: actuator.PCBA.ItemNumber,
            configNo: actuator.PCBA.ConfigNo,
            prodDateCode: actuator.PCBA.ProductionDateCode
        );
        _dbContext.Actuators.Add(EntityCreator.CreateActuatorModel(
            woNo: actuator.Id.WorkOrderNumber,
            serialNo: actuator.Id.SerialNumber,
            comProto: actuator.CommunicationProtocol,
            artNo: actuator.ArticleNumber,
            createdTime: actuator.CreatedTime,
            pcbaModel: pcba
        ));
        await _dbContext.SaveChangesAsync();
    }

    private async Task SetupPCBA(PCBA pcba)
    {
        _dbContext.Set<PCBAModel>().Add(new PCBAModel
        {
            Uid = pcba.Uid,
            ManufacturerNumber = pcba.ManufacturerNumber,
            Software = pcba.Software,
            ConfigNo = pcba.ConfigNo,
            ItemNumber = pcba.ItemNumber,
            ProductionDateCode = pcba.ProductionDateCode
        });
        await _dbContext.SaveChangesAsync();
    }

    private Domain.Entities.Actuator CreateActuator()
    {
        return Domain.Entities.Actuator.Create(_fixture.Create<CompositeActuatorId>(), _fixture.Create<PCBA>(),
            _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>(),
            _fixture.Create<DateTime>());
    }

    private PCBA CreatePCBA()
    {
        return PCBA.Create(_fixture.Create<string>(), _fixture.Create<int>(), _fixture.Create<string>(),
            _fixture.Create<string>(), _fixture.Create<int>(), _fixture.Create<string>());
    }


    private async Task AssertActuatorEquals(Domain.Entities.Actuator expected, Domain.Entities.Actuator actual)
    {
        Assert.Equal(expected.Id.SerialNumber, actual.Id.SerialNumber);
        Assert.Equal(expected.Id.WorkOrderNumber, actual.Id.WorkOrderNumber);
        Assert.Equal(expected.PCBA.Uid, actual.PCBA.Uid);
        Assert.Equal(expected.PCBA.ManufacturerNumber, actual.PCBA.ManufacturerNumber);
    }
}