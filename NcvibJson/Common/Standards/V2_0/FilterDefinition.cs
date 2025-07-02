using System.Text.Json.Serialization;

namespace NcvibJson.Common.Standards.V2_0;

public class FilterDefinition
{
    [JsonPropertyName("lowPass")] public double LowPass { get; set; }
    [JsonPropertyName("highPass")] public double HighPass { get; set; }
}