using System.Runtime.Serialization;

namespace NcvibJson.Common.Definitions.V2_0;

public enum Axis
{
    [EnumMember(Value = "V")] Vertical,
    [EnumMember(Value = "L")] Longitudinal,
    [EnumMember(Value = "T")] Transversal
}