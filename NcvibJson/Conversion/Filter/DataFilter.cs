namespace NcvibJson.Conversion.Filter;

public class DataFilter
{
    public string? LicenseKey { get; set; }
    
    public string? InputFilePath { get; set; }
    public string? OutputFilePath { get; set; }
    
    /// <summary>
    /// This switch sets the total number of significant digits to use for a value. It's a form of relative precision, as the position of the decimal point can change.
    /// </summary>
    public int? OverrideMaxNumberOfDigitsForDataValues { get; set; }
    
    /// <summary>
    /// Number of decimal digits to use for a value.
    /// </summary>
    public int? OverrideResolutionForDataValues { get; set; }
    
    /// <summary>
    /// To specify decimal separator in the output file, other than what the input file declares.
    /// </summary>
    public string? OverrideDecimalSeparatorInOutputFile { get; set; }
    
    /// <summary>
    /// Use the time zone information in the data files for presentation of date/time.
    /// </summary>
    public bool TimeZoneInformationFromDataFile { get; set; }
    
    /// <summary>
    /// Correction for microphones placed near walls.
    /// </summary>
    public bool DecibelCorrection { get; set; }

    public bool QuietMode { get; set; } = true;
    public bool CreateLogFiles { get; set; } = false;
    public bool CheckedFileNamesAgainstDataInTheFiles { get; set; } = false;
}