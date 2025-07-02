using System.Text.Json;
using System.Text.Json.Serialization;

namespace NcvibJson.Common.Standards.V2_0;

public class StandardFilterJsonConverter : JsonConverter<StandardFilter>
{
    public override StandardFilter? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return null;
            case JsonTokenType.String:
            {
                var enumValue = reader.GetString();

                if (!Enum.TryParse<PredefinedFilterType>(enumValue, out var filterType))
                {
                    throw new JsonException($"Cannot convert '{enumValue}' to StandardFilter");
                }
            
                var predefinedFilter = PredefinedFilters.GetFilter(filterType);
                
                return new StandardFilter
                {
                    Name = predefinedFilter.Name,
                    FilterDefinition = predefinedFilter.FilterDefinition
                };
            }
            case JsonTokenType.StartObject:
                return JsonSerializer.Deserialize<StandardFilter>(ref reader, options);
            case JsonTokenType.None:
            case JsonTokenType.EndObject:
            case JsonTokenType.StartArray:
            case JsonTokenType.EndArray:
            case JsonTokenType.PropertyName:
            case JsonTokenType.Comment:
            case JsonTokenType.Number:
            case JsonTokenType.True:
            case JsonTokenType.False:
            default:
                throw new JsonException($"Cannot convert {reader.TokenType} to StandardFilter");
        }
    }

    public override void Write(Utf8JsonWriter writer, StandardFilter? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }
    
        // Create a temporary object without the JsonConverter attribute
        var tempObject = new 
        {
            name = value.Name,
            filterDefinition = value.FilterDefinition
        };
    
        JsonSerializer.Serialize(writer, tempObject, options);
    }
}