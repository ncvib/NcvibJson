using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace NcvibJson.Common.Definitions.V2_0;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Axis
{
    [EnumMember(Value = "V")] Vertical,
    [EnumMember(Value = "L")] Longitudinal,
    [EnumMember(Value = "T")] Transversal
}