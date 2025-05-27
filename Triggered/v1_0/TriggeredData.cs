using System.Text.Json.Serialization;

namespace NcvibJson.Triggered.v1_0;

public class TriggeredData
{
    [JsonPropertyName("formatVersion")] public required string FormatVersion { get; set; }
    [JsonPropertyName("instrumentCompany")] public required string InstrumentCompany { get; set; }
    [JsonPropertyName("instrumentType")] public required string InstrumentType { get; set; } // LoggerType
    [JsonPropertyName("serialNumber")] public required string SerialNumber { get; set; }
    [JsonPropertyName("sensorType")] public string? SensorType { get; set; }
    [JsonPropertyName("port")] public int? Port { get; set; }
    [JsonPropertyName("startTime")] public required DateTimeOffset Time { get; set; }
    [JsonPropertyName("sampleRate")] public required double SampleRate { get; set; }
    [JsonPropertyName("preTriggerSamples")] public int? PreTriggerSamples { get; set; } // Samples per seconds.
    [JsonPropertyName("preTriggerRecordTime")]  double? PreTriggerRecordTime { get; set; }
    [JsonPropertyName("axes")] public List<string>? Axes { get; set; }
    [JsonPropertyName("transformationMatrix")] public List<double>? TransformationMatrix { get; set; }
    [JsonPropertyName("triggerLevel")] public List<double>? TriggerLevel { get; set; }
    [JsonPropertyName("triggerLevelUnit")] public string? TriggerLevelUnit { get; set; }
    [JsonPropertyName("values")] public List<ValueItem>? Values { get; set; }
    [JsonPropertyName("filterProfile")] public string? FilterProfile { get; set; }
    [JsonPropertyName("filterDefinition")] public FilterDefinitionItem? FilterDefinition { get; set; }
    [JsonPropertyName("coordinate")] public List<double>? Coordinate { get; set; }
    [JsonPropertyName("battery")] public string? Battery { get; set; }
    [JsonPropertyName("temperature")] public string? Temperature { get; set; }
    [JsonPropertyName("overload")] public bool? Overload { get; set; }
    [JsonPropertyName("sampleEntity")] public required string SampleEntity { get; set; }
    [JsonPropertyName("sampleUnits")] public List<string> SampleUnits { get; set; } = [];
    [JsonPropertyName("sampleSource")] public string? SampleSource { get; set; }
    [JsonPropertyName("samples")] public List<List<double>> Samples { get; set; } = [];
    
    public class FilterDefinitionItem
    {
        [JsonPropertyName("highPassFrequency")] public required string HighPassFrequency { get; set; }
        [JsonPropertyName("highPassOrder")] public required string HighPassOrder { get; set; }
        [JsonPropertyName("lowPassFrequency")] public required string LowPassFrequency { get; set; }
        [JsonPropertyName("lowPassOrder")] public required string LowPassOrder { get; set; }
        [JsonPropertyName("family")] public required string Family { get; set; }
    }

    public class ValueItem
    {
        [JsonPropertyName("value")] public List<double> Value { get; set; } = [];
        [JsonPropertyName("quantity")] public required string Entity { get; set; }
        [JsonPropertyName("unit")] public required string Unit { get; set; }
        [JsonPropertyName("timeOffsetInSeconds")] public string? TimeOffsetInSeconds { get; set; }
    }
}