namespace NcvibJson.Conversion.Filter;

[Flags]
public enum CommonConverterCapabilities
{
    None = 0,
    OverrideMaxNumberOfDigits = 1,
    OverrideResolution = 2,
    OverrideDecimalSeparator = 4,
    OverrideDataValueSeparator = 8,
    TimeZoneInformationFromDataFile = 16,
    DecibelCorrection = 32,
    QuietMode = 64,
    CreateLogFiles = 128,
    CheckFileNamesAgainstData = 256,
    All = int.MaxValue
}