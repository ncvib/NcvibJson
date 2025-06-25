using System.Text.Json.Serialization;

namespace NcvibJson.Common.Definitions.V2_0;

public class InstrumentDefinition
{
    [JsonPropertyName("formatVersion")] public required string FormatVersion { get; set; } = "2.0";
    [JsonPropertyName("company")] public required string Company { get; set; }
    [JsonPropertyName("type")] public required string Type { get; set; }
    [JsonPropertyName("serialNumber")]public required int SerialNumber { get; set; }
}