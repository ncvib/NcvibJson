namespace NcvibJson.Conversion.Filter.Triggered;

[Flags]
public enum TransientConverterCapabilities
{
    None = 0,
    OverrideStandard = 1,
    TimeDomainSwitching = 2,      
    InhibitRms = 4,           
    LowPassFiltering = 8,
    HighPassFiltering = 16,
    FastFourierTransformation = 32,  
    FastFourierTransformWindowing = 64,
    UseLinearFrequencyScale = 128,
    UseLogarithmicScale = 256,
    SpecifyRange = 512,
    SpecifyXLabel = 1024,
    SpecifyYLabel = 2048,
    SpecifyTitle = 4096,
    SpecifyFastFourierTransformTimeRange = 8192,
    OverrideSineWaveFrequency = 16384,
    All = int.MaxValue
}