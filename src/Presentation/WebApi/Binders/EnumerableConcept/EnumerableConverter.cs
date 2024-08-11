using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Binders.EnumerableConcept
{
    public class EnumerableConverter<T> : JsonConverter<IEnumerable<T>>
    {
        public override IEnumerable<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                return value?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => (T)Convert.ChangeType(s.Trim(), typeof(T)))
                            .ToArray();
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, IEnumerable<T> value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(string.Join(",", value));
        }
    }
}
