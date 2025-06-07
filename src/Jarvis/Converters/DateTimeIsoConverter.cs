using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jarvis.Converters
{
    public class DateTimeIsoConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();

            if (DateTime.TryParse(dateString, out var result))
            {
                if (result.Kind == DateTimeKind.Unspecified)
                {
                    return DateTime.SpecifyKind(result, DateTimeKind.Utc);
                }
                return result;
            }

            throw new JsonException($"Unable to convert \"{dateString}\" to DateTime");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var utcDateTime = value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime();

            var isoString = utcDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            writer.WriteStringValue(isoString);
        }
    }

    public class NullableDateTimeIsoConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            var dateString = reader.GetString();

            if (DateTime.TryParse(dateString, out var result))
            {
                if (result.Kind == DateTimeKind.Unspecified)
                {
                    return DateTime.SpecifyKind(result, DateTimeKind.Utc);
                }
                return result;
            }

            throw new JsonException($"Unable to convert \"{dateString}\" to DateTime?");
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            var utcDateTime = value.Value.Kind == DateTimeKind.Utc ? value.Value : value.Value.ToUniversalTime();
            var isoString = utcDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            writer.WriteStringValue(isoString);
        }
    }

}
