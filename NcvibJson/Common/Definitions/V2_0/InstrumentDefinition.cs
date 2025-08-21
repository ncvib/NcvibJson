using System.Text.Json.Serialization;

namespace NcvibJson.Common.Definitions.V2_0;

public class InstrumentDefinition
{
    [JsonPropertyName("formatVersion")] public required string FormatVersion { get; set; } = "2.0";
    [JsonPropertyName("company")] public required string Company { get; set; }
    [JsonPropertyName("loggerType")] public string? LoggerType { get; set; }
    [JsonPropertyName("sensorType")] public required string SensorType { get; set; }
    [JsonPropertyName("loggerSerialNumber")]public int? LoggerSerialNumber { get; set; }
    [JsonPropertyName("sensorSerialNumber")]public required int SensorSerialNumber { get; set; }
}