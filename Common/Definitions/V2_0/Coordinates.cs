using System.Text.Json.Serialization;

namespace NcvibJson.Common.Definitions.V2_0;

public class Coordinates
{
    [JsonPropertyName("longitude")] public double Longitude { get; set; }
    [JsonPropertyName("latitude")] public double Latitude { get; set; }
    [JsonPropertyName("elevationInMeters")] public double? ElevationInMeters { get; set; }
}