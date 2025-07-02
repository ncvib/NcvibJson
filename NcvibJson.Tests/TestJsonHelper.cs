using System.Text;
using System.Text.Json;

namespace NcvibJson.Tests;

public class TestJsonHelper
{
    public static string ReplaceValue(string serialized, string propertyName, object? value, bool remove = false)
    {
        using var document = JsonDocument.Parse(serialized);
        using var ms = new MemoryStream();
        using var writer = new Utf8JsonWriter(ms, new JsonWriterOptions { Indented = true });
        writer.WriteStartObject();
        
        foreach (var property in document.RootElement.EnumerateObject())
        {
            if (property.Name != propertyName)
            {
                property.WriteTo(writer);
            }
            else if(!remove)
            {
                writer.WritePropertyName(propertyName);

                switch (value)
                {
                    case null:
                        writer.WriteNullValue();
                        break;
                    case string stringValue:
                        writer.WriteStringValue(stringValue);
                        break;
                    case bool boolValue:
                        writer.WriteBooleanValue(boolValue);
                        break;
                }
            }
        }
        
        writer.WriteEndObject();
        writer.Flush();
    
        var modifiedJson = Encoding.UTF8.GetString(ms.ToArray());
        return modifiedJson;
    }
}