using System.Text;
using System.Text.Json;

namespace NcvibJson.Tests;

public class TestJsonHelper
{
    public static string ReplaceValue(string serialized, string propertyPath, object? value, bool remove = false)
    {
        using var document = JsonDocument.Parse(serialized);
        using var ms = new MemoryStream();
        using var writer = new Utf8JsonWriter(ms, new JsonWriterOptions {Indented = true});

        // Split the property path into segments (e.g., "nodeConfigurations[0].standard")
        var pathSegments = propertyPath.Split('.');
        ProcessJsonElement(document.RootElement, writer, pathSegments, 0, value, remove);

        writer.Flush();
        return Encoding.UTF8.GetString(ms.ToArray());
    }

    private static void ProcessJsonElement(JsonElement element, Utf8JsonWriter writer, string[] pathSegments, int currentSegmentIndex, object? newValue, bool remove)
    {
        if (currentSegmentIndex >= pathSegments.Length)
        {
            // We've gone past the path depth, just write the element as is
            element.WriteTo(writer);
            return;
        }

        var currentSegment = pathSegments[currentSegmentIndex];
        var isLastSegment = currentSegmentIndex == pathSegments.Length - 1;

        // Handle array indexing like nodeConfigurations[0]
        var arrayIndex = -1;

        if (currentSegment.Contains('[') && currentSegment.EndsWith(']'))
        {
            var startIndex = currentSegment.IndexOf('[');
            if (int.TryParse(currentSegment.Substring(startIndex + 1, currentSegment.Length - startIndex - 2), out arrayIndex))
            {
                currentSegment = currentSegment.Substring(0, startIndex);
            }
        }

        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                writer.WriteStartObject();

                foreach (var property in element.EnumerateObject())
                {
                    if (property.Name == currentSegment)
                    {
                        if (isLastSegment)
                        {
                            // This is the property we want to replace
                            if (!remove)
                            {
                                writer.WritePropertyName(property.Name);
                                WriteValue(writer, newValue);
                            }
                        }
                        else
                        {
                            // Continue down the path
                            writer.WritePropertyName(property.Name);
                            ProcessJsonElement(property.Value, writer, pathSegments, currentSegmentIndex + 1, newValue, remove);
                        }
                    }
                    else
                    {
                        // Not the property we're looking for, copy as is
                        property.WriteTo(writer);
                    }
                }

                writer.WriteEndObject();
                break;

            case JsonValueKind.Array:
                writer.WriteStartArray();

                var i = 0;
                foreach (var item in element.EnumerateArray())
                {
                    if (arrayIndex == -1 || arrayIndex == i)
                    {
                        if (arrayIndex != -1 && isLastSegment)
                        {
                            // Replace the entire array element
                            if (!remove)
                            {
                                WriteValue(writer, newValue);
                            }
                        }
                        else if (arrayIndex != -1)
                        {
                            // Process this specific array element
                            ProcessJsonElement(item, writer, pathSegments, currentSegmentIndex + 1, newValue, remove);
                        }
                        else
                        {
                            // Process all array elements
                            ProcessJsonElement(item, writer, pathSegments, currentSegmentIndex, newValue, remove);
                        }
                    }
                    else
                    {
                        // Not the array index we're looking for
                        item.WriteTo(writer);
                    }

                    i++;
                }

                writer.WriteEndArray();
                break;

            case JsonValueKind.Undefined:
            case JsonValueKind.String:
            case JsonValueKind.Number:
            case JsonValueKind.True:
            case JsonValueKind.False:
            case JsonValueKind.Null:
            default:
                // For other value types, just write them directly
                element.WriteTo(writer);
                break;
        }
    }

    private static void WriteValue(Utf8JsonWriter writer, object? value)
    {
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
            case int intValue:
                writer.WriteNumberValue(intValue);
                break;
            case long longValue:
                writer.WriteNumberValue(longValue);
                break;
            case double doubleValue:
                writer.WriteNumberValue(doubleValue);
                break;
            case decimal decimalValue:
                writer.WriteNumberValue(decimalValue);
                break;
            case JsonElement jsonElement:
                jsonElement.WriteTo(writer);
                break;
            default:
            {
                // For other types, serialize to JSON first
                var json = JsonSerializer.Serialize(value);
                using var doc = JsonDocument.Parse(json);
                doc.RootElement.WriteTo(writer);
                break;
            }
        }
    }
}