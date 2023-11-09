using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Backend.Tests;

public class MockConfigurationSection : IConfigurationSection
{
    private readonly Dictionary<string, string> values = new()
    {
        { "WorkOrderNumberLength", "1" },
        { "SerialNumberMinLength", "2" },
        { "SerialNumberMaxLength", "3" }
    };

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
        
        return null;
    }

    public string? this[string key]
    {
        get => values[key];
        set => throw new NotImplementedException();
    }

    public string Key { get; }
    public string Path { get; }
    public string? Value { get; set; }
}