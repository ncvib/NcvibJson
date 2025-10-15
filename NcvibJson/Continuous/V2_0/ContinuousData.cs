using System.Text.Json.Serialization;
using NcvibJson.Common.Definitions.V2_0;

namespace NcvibJson.Continuous.V2_0;

public class ContinuousData
{
    [JsonPropertyName("formatVersion")] public required string FormatVersion { get; set; }
    [JsonPropertyName("instrument")] public required InstrumentDefinition InstrumentDefinition { get; set; }
    [JsonPropertyName("port")]public required int Port { get; set; } // NorSonic Channel
    [JsonPropertyName("startTime")] public required DateTimeOffset StartTime { get; set; }
    [JsonPropertyName("intervalTimeInSeconds")] public required int IntervalTimeInSeconds { get; set; }
    [JsonPropertyName("quantity")]public required string Quantity { get; set; }
    [JsonPropertyName("unit")]public required string  Unit { get; set; } // m/s, m/s^2, etc.
    [JsonPropertyName("axes")] public List<Axis> Axes { get; set; } = [];

    [JsonPropertyName("samples")] public required List<Sample> Samples { get; set; } = [];

    public class Sample
    {
        [JsonPropertyName("time")] public required string Time { get; set; }
        [JsonPropertyName("values")] public required List<double> Values { get; set; } = [];
    }
}