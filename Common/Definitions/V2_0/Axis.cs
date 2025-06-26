using System.Text.Json.Serialization;

namespace NcvibJson.Common.Definitions.V2_0;

public enum Axis
{
    [JsonPropertyName("V")] Vertical,
    [JsonPropertyName("L")] Longitudinal,
    [JsonPropertyName("T")] Transversal
}