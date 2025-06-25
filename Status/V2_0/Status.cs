using System.Text.Json.Serialization;
using NcvibJson.Common.Definitions.V2_0;

namespace NcvibJson.Status.V2_0;

public class Status
{
    [JsonPropertyName("formatVersion")] public required string FormatVersion { get; set; } = "2.0";
    [JsonPropertyName("instrument")] public required InstrumentDefinition InstrumentDefinition { get; set; }
    [JsonPropertyName("temperatureCelsius")] public double? TemperatureCelsius { get; set; }
    [JsonPropertyName("batteryLevelVoltage")] public double? BatteryLevelVoltage { get; set; }
    [JsonPropertyName("batteryLevelPercentage")] public double? BatteryLevelPercentage { get; set; }
    [JsonPropertyName("signalStrength")] public double? SignalStrength { get; set; }
    [JsonPropertyName("availableMemory")] public double? AvailableMemory { get; set; }
    [JsonPropertyName("statusTimeInUtc")] public DateTimeOffset? StatusTimeInUtc { get; set; }
    [JsonPropertyName("latestDataDeliveryInUtc")] public DateTimeOffset? LatestDataDeliveryInUtc { get; set; }
    [JsonPropertyName("monitoringActive")] public bool? MonitoringActive { get; set; }
    
    [JsonPropertyName("coordinates")] public Coordinates? Coordinates { get; set; }
    [JsonPropertyName("nodes")] public Node[] Nodes { get; set; } = [];
    
    public class Node
    {
        [JsonPropertyName("instrument")] public required InstrumentDefinition InstrumentDefinition { get; set; }
        [JsonPropertyName("axis")] public Axis? Axis { get; set; }
        [JsonPropertyName("status")] public NodeStatus? Status { get; set; }
    }
    
    public enum NodeStatus
    {
        Unknown,
        Active,
        Inactive,
        Lost
    }
}
