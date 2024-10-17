using Newtonsoft.Json;

namespace discipline.core.Serializer;

internal sealed class NewtonsoftJsonSerializer(
) : ISerializer
{
    private readonly JsonSerializerSettings _settings = new()
    {
        NullValueHandling = NullValueHandling.Ignore
    };

    public string ToJson(object data)
        => JsonConvert.SerializeObject(data, _settings);

    public T ToObject<T>(string json) where T : class
        => JsonConvert.DeserializeObject<T>(json);
}