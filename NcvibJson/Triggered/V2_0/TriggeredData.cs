using System.Text.Json.Serialization;
using NcvibJson.Common.Definitions.V2_0;
using NcvibJson.Common.Standards.V2_0;

namespace NcvibJson.Triggered.V2_0;

public class TriggeredData
{
    [JsonPropertyName("formatVersion")] public required string FormatVersion { get; set; } = "2.0";
    [JsonPropertyName("instrument")] public required InstrumentDefinition InstrumentDefinition { get; set; }
    [JsonPropertyName("port")] public int? Port { get; set; } = 0;
    [JsonPropertyName("coordinates")] public Coordinates? Coordinates { get; set; }
    [JsonPropertyName("battery")] public string? Battery { get; set; }
    [JsonPropertyName("temperature")] public string? Temperature { get; set; }
    [JsonPropertyName("overload")] public bool? Overload { get; set; }
    [JsonPropertyName("filteredSamples")] public bool FilteredSamples { get; set; }
    [JsonPropertyName("standard")] public StandardFilter? Standard { get; set; }
    [JsonPropertyName("measuredQuantity")] [JsonConverter(typeof(JsonStringEnumConverter))] public required MeasuredQuantityType MeasuredQuantity { get; set; }
    [JsonPropertyName("measuredUnit")] public required string MeasuredUnit { get; set; }
    [JsonPropertyName("measuredMaxValue")] public List<double> MeasuredMaxValue { get; set; } = [];
    [JsonPropertyName("peakParticleVelocity")] public List<double> PeakParticleVelocity { get; set; } = [];
    [JsonPropertyName("peakParticleVelocityUnit")] public string? PeakParticleVelocityUnit { get; set; } = "mm/s";
    [JsonPropertyName("peakParticleAcceleration")] public List<double> PeakParticleAcceleration { get; set; } = [];
    [JsonPropertyName("peakParticleAccelerationUnit")] public string? PeakParticleAccelerationUnit { get; set; } = "mm/s^2";
    [JsonPropertyName("peakParticleDisplacement")] public List<double> PeakParticleDisplacement { get; set; } = [];
    [JsonPropertyName("peakParticleDisplacementUnit")] public string? PeakParticleDisplacementUnit { get; set; } = "mm";
    [JsonPropertyName("zeroCuttingFrequencyVelocity")] public List<double> ZeroCuttingFrequencyVelocity { get; set; } = [];
    [JsonPropertyName("zeroCuttingFrequencyVelocityUnit")] public string? ZeroCuttingFrequencyVelocityUnit { get; set; } = "Hz";
    [JsonPropertyName("zeroCuttingFrequencyAcceleration")] public List<double> ZeroCuttingFrequencyAcceleration { get; set; } = [];
    [JsonPropertyName("zeroCuttingFrequencyAccelerationUnit")] public string? ZeroCuttingFrequencyAccelerationUnit { get; set; } = "Hz";
    [JsonPropertyName("startTime")] public required DateTime StartTime { get; set; }
    [JsonPropertyName("sampleRate")] public required double SampleRate { get; set; }
    [JsonPropertyName("numberOfPreTriggerSamples")] public int NumberOfPreTriggerSamples { get; set; }
    [JsonPropertyName("axes")] public List<Axis> Axes { get; set; } = [];
    [JsonPropertyName("transformationMatrix")] public List<double> TransformationMatrix { get; set; } = [];
    [JsonPropertyName("triggerLevel")] public List<double> TriggerLevel { get; set; } = [];
    [JsonPropertyName("triggerLevelUnit")] public string? TriggerLevelUnit { get; set; }
    [JsonPropertyName("maxSamples")] public List<SampleItem> MaxSamples { get; set; } = [];
    [JsonPropertyName("sampleUnits")] public required List<string> SampleUnits { get; set; } = [];
    [JsonPropertyName("samples")] public required List<List<double>> Samples { get; set; } = [];

    public enum MeasuredQuantityType
    {
        Velocity,
        Acceleration,
        Displacement,
        Voltage,
        SoundPressure,
        SoundIntensity,
        SoundLevel,
        VibrationLevel
    }

    public class SampleItem
    {
        [JsonPropertyName("axis")] public required Axis Axis { get; set; }
        [JsonPropertyName("quantity")] public required string Quantity { get; set; }
        [JsonPropertyName("unit")] public required string Unit { get; set; }
        [JsonPropertyName("value")] public List<double> Value { get; set; } = [];
        [JsonPropertyName("timeOffsetInSeconds")] public double? TimeOffsetInSeconds { get; set; }
    }
}