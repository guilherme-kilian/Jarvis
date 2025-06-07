using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jarvis.Converters
{
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString()!);
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTime dateTimeValue,
            JsonSerializerOptions options)
        {
            writer.WriteStringValue(dateTimeValue.ToString("yyyy-MM-ddThh:MM:ss"));
        }
    }
}
