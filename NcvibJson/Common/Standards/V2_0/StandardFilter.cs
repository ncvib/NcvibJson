using System.Text.Json.Serialization;

namespace NcvibJson.Common.Standards.V2_0;

[JsonConverter(typeof(StandardFilterJsonConverter))]
public class StandardFilter
{
    [JsonPropertyName("name")] public required string Name { get; set; } = "Unknown";
    [JsonPropertyName("filterDefinition")] public required FilterDefinition FilterDefinition { get; set; } = new() {LowPass = 0, HighPass = 1000};
}