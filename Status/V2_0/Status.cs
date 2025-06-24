using System.Text.Json.Serialization;

namespace NcvibJson.Status.V2_0;

public class Status
{
    [JsonPropertyName("formatVersion")] public required string FormatVersion { get; set; } = "2.0";
    [JsonPropertyName("batteryLevelVoltage")] public double? BatteryLevelVoltage { get; set; }
    [JsonPropertyName("batteryLevelPercentage")] public double? BatteryLevelPercentage { get; set; }
    [JsonPropertyName("signalStrength")] public double? SignalStrength { get; set; }
    [JsonPropertyName("availableMemory")] public double? AvailableMemory { get; set; }
    [JsonPropertyName("statusTimeInUtc")] public DateTimeOffset? StatusTimeInUtc { get; set; }
    [JsonPropertyName("latestDataDeliveryInUtc")] public DateTimeOffset? LatestDataDeliveryInUtc { get; set; }
    [JsonPropertyName("monitoringActive")] public bool? MonitoringActive { get; set; }
    
    [JsonPropertyName("position")] public Coordinates? Position { get; set; }
    [JsonPropertyName("nodeStatuses")] public NodeStatus[] NodeStatuses { get; set; } = [];
    
    public class Coordinates
    {
        [JsonPropertyName("longitude")] public double Longitude { get; set; }
        [JsonPropertyName("latitude")] public double Latitude { get; set; }
        [JsonPropertyName("elevationInMeters")] public double? ElevationInMeters { get; set; }
    }
    
    public class NodeStatus
    {
        [JsonPropertyName("axis")] public string? Axis { get; set; }
        [JsonPropertyName("active")] public bool? Active { get; set; }
    }        
}