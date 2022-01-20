using System.Text.Json;

namespace EvaLabs.Helper.ExtensionMethod
{
    public static class JsonExtension
    {
        public static string ToJson<TSource>(this TSource data)
        {
            return JsonSerializer.Serialize(data);
        }

        public static TResult FromJson<TResult>(this string json)
        {
            return JsonSerializer.Deserialize<TResult>(json);
        }
    }
}