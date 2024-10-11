namespace discipline.core.Serializer;

public interface ISerializer
{
    string ToJson(object data);
    T ToObject<T>(string json) where T : class;
}