namespace Diablo3Kit;

public static class Utils
{

    /// <summary>
    /// JSON序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="JsonObj"></param>
    /// <returns></returns>

    public static string JsonSerialize(this object JsonObj)
    {
        string result = string.Empty;
        var serializer = new DataContractSerializer(JsonObj.GetType());
        using (var stream = new MemoryStream())
        {
            serializer.WriteObject(stream, JsonObj);
            result = Encoding.UTF8.GetString(stream.ToArray());
        }
        return result;
    }



    /// <summary>
    /// JSON反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="JsonObj"></param>
    /// <returns></returns>

    public static T JsonDerialize<T>(this string JsonStr)
    {
        T resultObject = Activator.CreateInstance<T>();
        var serializer = new DataContractSerializer(typeof(T));
        using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(JsonStr)))
        {
            resultObject = (T)serializer.ReadObject(stream);
        }

        return resultObject;
    }
}
