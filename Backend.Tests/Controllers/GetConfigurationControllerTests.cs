﻿using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Backend.Controllers;
using Backend.Tests.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Backend.Tests.Controllers;

public class GetConfigurationControllerTests
{
    private Fixture _fixture = new();

    private IConfiguration _configuration = GetConfig.GetTestConfig();
    private GetConfigurationController _controller;

    public GetConfigurationControllerTests()
    {
        _controller = new GetConfigurationController(_configuration);
    }

    [Fact]
    public async Task GetAsync_ReturnsConfigurationResponse_WhenCalled()
    {
        var result = _controller.GetAsync(CancellationToken.None) as ObjectResult;
        var response = result.Value;
        Assert.NotNull(result);
        Assert.IsType<ConfigurationResponse>(response);
        Assert.IsType<OkObjectResult>(result);
    }
}