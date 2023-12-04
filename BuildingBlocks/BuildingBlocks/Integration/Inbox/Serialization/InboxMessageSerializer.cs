using BuildingBlocks.Application;
using Newtonsoft.Json;

namespace BuildingBlocks.Integration.Inbox.Serialization;

public class InboxMessageSerializer
{
    private readonly JsonSerializerSettings settings;

    public InboxMessageSerializer()
    {
        settings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateResolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        };
    }

    public string Serialize(ICommand command, Type? type)
    {
        return JsonConvert.SerializeObject(command, type, settings);
    }

    public object? Deserialize(string payload, string messageType)
    {
        Type? type = Type.GetType(messageType);
        return JsonConvert.DeserializeObject(payload, type, settings);
    }
}