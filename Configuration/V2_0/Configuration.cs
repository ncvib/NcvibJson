using System.Text.Json.Serialization;
using NcvibJson.Common.Definitions.V2_0;
using NcvibJson.Common.Standards.V2_0;

namespace NcvibJson.Configuration.V2_0;

public class Configuration
{
    [JsonPropertyName("formatVersion")] public required string FormatVersion { get; set; } = "2.0";
    [JsonPropertyName("instrument")] public required InstrumentDefinition InstrumentDefinition { get; set; }
    [JsonPropertyName("instrumentIanaTimezone")] public string InstrumentIanaTimezone { get; set; } = "Etc/UTC";
    [JsonPropertyName("activeHours")] public List<int> ActiveHours { get; set; } = [..Enumerable.Range(0, 23)];

    [JsonPropertyName("nodeConfigurations")] public NodeConfiguration[] NodeConfigurations { get; set; } = [];

    public class NodeConfiguration
    {
        [JsonPropertyName("axis")] public Axis? Axis { get; set; }
        [JsonPropertyName("standard")] public StandardFilter? Standard { get; set; }
        [JsonPropertyName("intervalPeriodInSeconds")] public int IntervalPeriodInSeconds { get; set; } = 120;
        [JsonPropertyName("recordingPeriodInSeconds")] public int RecordingPeriodInSeconds { get; set; } = 5;
        [JsonPropertyName("threshold")] public double Threshold { get; set; } = 1.0;
        [JsonPropertyName("thresholdActive")] public bool ThresholdActive { get; set; } = true;
    }    
}