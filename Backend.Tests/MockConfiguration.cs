using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Backend.Tests;

public class MockConfiguration : IConfiguration
{
    public IEnumerable<IConfigurationSection> GetChildren()
    {
        throw new NotImplementedException();
    }

    public IChangeToken GetReloadToken()
    {
        throw new NotImplementedException();
    }

    public IConfigurationSection GetSection(string key)
    {
        return new MockConfigurationSection();
    }

    public string? this[string key]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }
}