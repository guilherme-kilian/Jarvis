using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class EnumStringConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:

                var stringValue = reader.GetString();
                if (Enum.TryParse<T>(stringValue, ignoreCase: true, out var enumFromString))
                {
                    return enumFromString;
                }
                throw new JsonException($"Unable to convert \"{stringValue}\" to enum {typeof(T).Name}");

            case JsonTokenType.Number:

                var intValue = reader.GetInt32();
                if (Enum.IsDefined(typeof(T), intValue))
                {
                    return (T)Enum.ToObject(typeof(T), intValue);
                }
                throw new JsonException($"Unable to convert {intValue} to enum {typeof(T).Name}");

            default:
                throw new JsonException($"Unexpected token type {reader.TokenType} when parsing enum");
        }
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString().ToUpper());
    }
}

public class EnumStringConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(EnumStringConverter<>).MakeGenericType(typeToConvert);

        var converter = (JsonConverter?)Activator.CreateInstance(converterType);

        if (converter is null)
            throw new InvalidOperationException("Enum converter cannot be null");

        return converter;
    }
}
