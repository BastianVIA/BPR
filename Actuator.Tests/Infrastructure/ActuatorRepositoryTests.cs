using Actuator.Tests.Util;
using AutoFixture;
using Domain.Entities;
using Infrastructure;

namespace Actuator.Tests.Infrastructure;

public class ActuatorRepositoryTests
{
    private Fixture _fixture = new();
    private ApplicationDbContext _dbContext = GetDbContext.GetContext();
    private ActuatorRepository _repository;

    public ActuatorRepositoryTests()
    {
        _repository = new ActuatorRepository(_dbContext);
    }
    
    [Fact]
    public async Task Create_Should_AddToDatabase()
    {
        var before = _dbContext.Actuators.Count();
        var actuator = _fixture.Create<Domain.Entities.Actuator>();
        await _repository.CreateActuator(actuator);
        
        Assert.Equal(before + 1, _dbContext.Actuators.Count());
    }

    [Fact]
    public async Task Create_AddsRightObject()
    {
        var actuator = _fixture.Create<Domain.Entities.Actuator>();
        await _repository.CreateActuator(actuator);
        var added = _dbContext.Actuators.First();
        
        Assert.Equal(actuator.Id.SerialNumber, added.SerialNumber);
        Assert.Equal(actuator.Id.WorkOrderNumber, added.WorkOrderNumber);
        Assert.Equal(actuator.PCBAUid, added.PCBAUid);
    }

    [Fact]
    public async Task Create_ShouldNotAddActuator_WhenAddFails()
    {
        var actuator = _fixture.Create<Domain.Entities.Actuator>();
        await _repository.CreateActuator(actuator);

        var before = _dbContext.Actuators.Count();
        
        await _repository.CreateActuator(actuator);
        Assert.Equal(before, _dbContext.Actuators.Count());
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
        await _repository.CreateActuator(expected);

        var result = await _repository.GetActuator(expected.Id);
        
        Assert.NotNull(result);
        Assert.IsType<Domain.Entities.Actuator>(result);
    }

    [Fact]
    public async Task GetActuator_ReturnsRightActuator_WhenActuatorFound()
    {
        var expected = _fixture.Create<Domain.Entities.Actuator>();
        _dbContext.Set<ActuatorModel>().Add(new ActuatorModel()
        {
            SerialNumber = expected.Id.SerialNumber,
            WorkOrderNumber = expected.Id.WorkOrderNumber,
            PCBAUid = expected.PCBAUid
        });
        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetActuator(expected.Id);
        
        Assert.NotNull(result);
        Assert.Equal(expected.Id.SerialNumber, result.Id.SerialNumber);
        Assert.Equal(expected.Id.WorkOrderNumber, result.Id.WorkOrderNumber);
        Assert.Equal(expected.PCBAUid, result.PCBAUid);
    }

    [Fact]
    public async Task Update_()
    {
        var init = _fixture.Create<Domain.Entities.Actuator>();
        _dbContext.Set<ActuatorModel>().Add(new ActuatorModel()
        {
            SerialNumber = init.Id.SerialNumber,
            WorkOrderNumber = init.Id.WorkOrderNumber,
            PCBAUid = init.PCBAUid
        });
        await _dbContext.SaveChangesAsync();
        
    }
}