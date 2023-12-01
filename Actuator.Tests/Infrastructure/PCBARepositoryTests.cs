using Actuator.Tests.Util;
using AutoFixture;
using BuildingBlocks.Infrastructure;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Actuator.Tests.Infrastructure;

public class PCBARepositoryTests
{
    private Fixture _fixture = new();
    private ApplicationDbContext _dbContext = GetDbContext.GetContext();
    private PCBARepository _repository;
    private IScheduler _scheduler = Substitute.For<IScheduler>();

    public PCBARepositoryTests()
    {
        _repository = new PCBARepository(_dbContext,_scheduler);
    }
    
    [Fact]
    public async Task Create_Should_AddToDatabase()
    {
        var countBefore = _dbContext.PCBAs.Count();
        var pcba = _fixture.Create<PCBA>();
        var model = new PCBA(pcba.Uid);
        
        await _repository.CreatePCBA(model);
        await _dbContext.SaveChangesAsync();
        
        var countAfter = _dbContext.PCBAs.Count();
        Assert.Equal(countBefore + 1, countAfter);
    }
    
    [Fact]
    public async Task Create_AddsRightObject()
    {
        var pcba = _fixture.Create<PCBA>();
        var model = new PCBA(pcba.Uid);

        await _repository.CreatePCBA(model);
        await _dbContext.SaveChangesAsync();
        
        var added = _dbContext.PCBAs.First();
        
        Assert.Equal(pcba.Uid, added.Uid);
    }
    
    [Fact]
    public async Task Create_ShouldNotAddPCBA_WhenAddFails()
    {
        var pcba = _fixture.Create<PCBA>();
        _dbContext.PCBAs.Add(new PCBAModel
        {
            Uid = pcba.Uid,
            ManufacturerNumber = pcba.ManufacturerNumber,
            ItemNumber = pcba.ItemNumber,
            Software = pcba.Software,
            ProductionDateCode = pcba.ProductionDateCode
        });
        await _dbContext.SaveChangesAsync();
        
        await Assert.ThrowsAnyAsync<Exception>(() => _repository.CreatePCBA(pcba));
    }

    [Fact]
    public async Task GetPCBA_ThrowsKeyNotFoundException_WhenPCBANotFound()
    {
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _repository.GetPCBA("N/A"));
    }
    
    [Fact]
    public async Task GetPCBA_ReturnsPCBA_WhenPCBAFound()
    {
        var expected = _fixture.Create<PCBA>();
        await SetupPCBA(expected);

        var result = await _repository.GetPCBA(expected.Uid);
        
        Assert.NotNull(result);
        Assert.IsType<PCBA>(result);
    }

    [Fact]
    public async Task GetPCBA_ReturnsRightPCBA_WhenPCBAFound()
    {
        var expected = _fixture.Create<PCBA>();
        await SetupPCBA(expected);
        
        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetPCBA(expected.Uid);
        
        Assert.NotNull(result);
        await AssertPCBAEquals(expected, result);
    }

    [Fact]
    public async Task Update_ShouldChangeCorrectPCBA_WhenUpdating()
    {
        var init = _fixture.Create<PCBA>();
        
        await SetupPCBA(init);
        var newPCBA = new PCBA(init.Uid, 123, "", "", 1);
        
        await _repository.UpdatePCBA(newPCBA);
        var result = await _dbContext.PCBAs.FirstOrDefaultAsync(p => p.Uid == newPCBA.Uid);
        
        Assert.NotNull(result);
        Assert.NotEqual(init.ManufacturerNumber, result.ManufacturerNumber);
    }

    [Fact]
    public async Task Update_ShouldChangeExistingPCBA_WhenUpdating()
    {
        var init = _fixture.Create<PCBA>();
        
        await SetupPCBA(init);
        var beforeCount = _dbContext.PCBAs.Count();
        var newPCBA = new PCBA(init.Uid, 123, "", "", 1);
        
        await _repository.UpdatePCBA(newPCBA);
        var result = _dbContext.PCBAs.First();
        
        Assert.NotNull(result);
        Assert.Equal(beforeCount, _dbContext.PCBAs.Count());
    }

    [Fact]
    public async Task Update_ShouldThrowException_WhenUpdatingFails()
    {
        var pcbaNotInDb = _fixture.Create<PCBA>();
        await Assert.ThrowsAnyAsync<Exception>(() => _repository.UpdatePCBA(pcbaNotInDb));
    }
    
    private async Task SetupPCBA(PCBA pcba)
    {
        _dbContext.Set<PCBAModel>().Add(new PCBAModel
        {
            Uid = pcba.Uid,
            ManufacturerNumber = pcba.ManufacturerNumber,
            ItemNumber = pcba.ItemNumber,
            Software = pcba.Software,
            ProductionDateCode = pcba.ProductionDateCode
        });
        await _dbContext.SaveChangesAsync();
    }

    private async Task AssertPCBAEquals(PCBA expected, PCBA actual)
    {
        Assert.Equal(expected.Uid, actual.Uid);
        Assert.Equal(expected.ManufacturerNumber, actual.ManufacturerNumber);
        Assert.Equal(expected.ItemNumber, actual.ItemNumber);
        Assert.Equal(expected.Software, actual.Software);
        Assert.Equal(expected.ProductionDateCode, actual.ProductionDateCode);
    }
}