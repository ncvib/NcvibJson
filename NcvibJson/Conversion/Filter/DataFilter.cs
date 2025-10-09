namespace NcvibJson.Conversion.Filter;

public class DataFilter
{
    public string? LicenseKey { get; set; }
    
    public string? InputFilePath { get; set; }
    public string? OutputFilePath { get; set; }
    public int? OverrideMaxNumberOfDigitsForDataValues { get; set; }
    public int? OverrideResolutionForDataValues { get; set; }
    public string? OverrideDecimalSeparatorInOutputFile { get; set; }
    public string? OverrideDataValueSeparatorInOutputFile { get; set; }
    public bool TimeZoneInformationFromDataFile { get; set; }
    public bool DecibelCorrection { get; set; }

    public bool QuietMode { get; set; } = true;
    public bool CreateLogFiles { get; set; } = false;
    public bool CheckedFileNamesAgainstDataInTheFiles { get; set; } = false;
}