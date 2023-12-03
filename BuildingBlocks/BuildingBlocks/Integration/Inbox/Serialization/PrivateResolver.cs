using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BuildingBlocks.Integration.Inbox.Serialization;

public class PrivateResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);
        if (!prop.Writable)
        {
            var property = member as PropertyInfo;
            var hasPrivateSetter = property?.GetSetMethod(nonPublic: true) != null;
            prop.Writable = hasPrivateSetter;
        }
        return prop;
    }
}