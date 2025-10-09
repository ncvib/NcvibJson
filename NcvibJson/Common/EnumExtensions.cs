using System.Runtime.Serialization;

namespace NcvibJson.Common;

public static class EnumExtensions
{
    public static string? ToEnumMemberValue(this Enum @enum)
    {
        var attr =
            @enum
                .GetType()
                .GetMember(@enum.ToString())
                .FirstOrDefault()?
                .GetCustomAttributes(false)
                .OfType<EnumMemberAttribute>()
                .FirstOrDefault();
            
        return attr == null ? @enum.ToString() : attr.Value;
    }
}