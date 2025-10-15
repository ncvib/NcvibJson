using NcvibJson.Common.Standards.V2_0;

namespace NcvibJson.Conversion.Filter.Triggered;

public class TriggeredDataFilter : DataFilter
{
    public PredefinedFilterType? Standard { get; set; }
    
    /// <summary>
    /// Specifies integration or differentiation of the transient
    /// </summary>
    public TimeDomain TimeDomain { get; set; }
    
    /// <summary>
    /// Inhibits the RMS algorithm in standards which normally uses RMS. It does not alter other filtering in the standard.
    /// </summary>
    public bool NoRms { get; set; }
    
    /// <summary>
    /// Specifies low-pass filtering of the transient, using a 2:nd order butterworth filter.
    /// </summary>
    public int? LowPass { get; set; }
    
    /// <summary>
    /// Specifies high-pass filtering of the transient, using a 2:nd order butterworth filter.
    /// </summary>
    public int? HighPass { get; set; }
    
    public FastFourierTransformType FastFourierTransform { get; set; }
    public FastFourierTransformWindow FastFourierTransformWindow { get; set; }
    
    /// <summary>
    /// First sample for FFT algorithm to work on, in seconds relative to trig. point.
    /// </summary>
    public double? FastFourierTransformFrom { get; set; }
    
    /// <summary>
    /// Last sample for FFT algorithm to work on, in seconds relative to trig. point.
    /// </summary>
    public double? FastFourierTransformTo { get; set; }
    
    /// <summary>
    /// Overrides the transient signal with a sine wave having the specified frequency
    /// </summary>
    public double? SineWaveFrequency { get; set; }

    /// <summary>
    /// Specifies linear frequency scale on FFT and ThirdOctave plots.
    /// </summary>
    public bool LinearFrequencyScale { get; set; } = true;

    /// <summary>
    /// Forces the data scale to be logarithmic when possible.
    /// </summary>
    public bool LogarithmicScale { get; set; } = false;
    
    /// <summary>
    /// Specifies manual setting of the plot range of the data axis.
    /// </summary>
    public int? Range { get; set; }
    
    /// <summary>
    /// Replaces the label of the time/frequency axis.
    /// </summary>
    public string? XLabel { get; set; }
    
    /// <summary>
    /// Replaces the label of the data axis.
    /// </summary>
    public string? YLabel { get; set; }
    
    /// <summary>
    /// Replaces the main label of the plot.
    /// </summary>
    public string? Title { get; set; }
}