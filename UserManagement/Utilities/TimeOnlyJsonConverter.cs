using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;
            int hour = root.GetProperty("hour").GetInt32();
            int minute = root.TryGetProperty("minute", out var minProp) ? minProp.GetInt32() : 0;
            return new TimeOnly(hour, minute);
        }

        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            if (string.IsNullOrEmpty(value))
                throw new JsonException("Time string cannot be null or empty.");
            return TimeOnly.Parse(value);
        }

        throw new JsonException($"Unexpected token parsing TimeOnly. Token: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("HH:mm:ss"));
    }
}
