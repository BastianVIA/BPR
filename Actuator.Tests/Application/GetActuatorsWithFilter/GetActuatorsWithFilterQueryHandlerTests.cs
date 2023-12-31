﻿using Actuator.Tests.Util;
using Application.GetActuatorsWithFilter;
using AutoFixture;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using NSubstitute;

namespace Actuator.Tests.Application.GetActuatorsWithFilter;

public class GetActuatorsWithFilterQueryHandlerTests
{
    private Fixture _fixture = new();
    private IActuatorRepository _repository = Substitute.For<IActuatorRepository>();
    private GetActuatorsWithFilterQueryHandler _handler;

    public GetActuatorsWithFilterQueryHandlerTests()
    {
        _handler = new GetActuatorsWithFilterQueryHandler(_repository);
    }

    [Fact]
    public async Task Handle_ReturnsGetActuatorsWithFilterDtoWithManyActuators_WhenWOHasManyMatches()
    {
        // Arrange
        var request = _fixture.Build<GetActuatorsWithFilterQuery>()
            .With(q => q.WorkOrderNumber, _fixture.Create<int>())
            .Create();
        var noOfActuatorsToReturn = 3;
        var expected = new List<Domain.Entities.Actuator>();
        expected.AddMany(() => EntityCreator.CreateActuator(woNo: request.WorkOrderNumber), noOfActuatorsToReturn);
        
        _repository.GetActuatorsWithFilter(Arg.Any<int>(), Arg.Any<int?>(), Arg.Any<string?>(), Arg.Any<string>(),
            Arg.Any<int?>(), Arg.Any<int?>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<string?>(),
            Arg.Any<string?>(), Arg.Any<DateTime?>(), Arg.Any<DateTime?>())
            .Returns(expected);
        // Act
        var result = await _handler.Handle(request, CancellationToken.None);
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.ActuatorDtos);
        foreach (var dto in result.ActuatorDtos)   
        {
            Assert.Equal(request.WorkOrderNumber, dto.WorkOrderNumber);
        }
    }
}