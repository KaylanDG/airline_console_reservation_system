using System;
using System.Globalization; // Required for CultureInfo
using System.Text.Json;
using System.Text.Json.Serialization; // Add this line


public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string dateString = reader.GetString();

        // Specify the expected date format: "dd-MM-yyyy HH:mm tt"
        return DateTime.ParseExact(dateString, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
