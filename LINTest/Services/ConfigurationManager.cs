﻿using Microsoft.Extensions.Configuration;

namespace LINTest.Services;
public class ConfigurationManager
{
    public int RunIntervalInSeconds { get; private set; }

    public ConfigurationManager(IConfiguration configuration)
    {
        var linTestConfig = configuration.GetSection("LINTest");
        RunIntervalInSeconds = linTestConfig.GetValue<int>("RunIntervalInSeconds");
    }
}
