using System.Text.Json.Serialization;

namespace NcvibJson.Common.Standards.V2_0;

[JsonConverter(typeof(StandardFilterJsonConverter))]
public class StandardFilter
{
    [JsonPropertyName("name")] public required string Name { get; set; }
    [JsonPropertyName("filterDefinition")] public required FilterDefinition FilterDefinition { get; set; }
    
    // Empty constructor for deserialization
    public StandardFilter()
    {
    }
}