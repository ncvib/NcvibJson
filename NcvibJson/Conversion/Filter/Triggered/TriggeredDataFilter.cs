namespace NcvibJson.Conversion.Filter.Triggered;

public class TriggeredDataFilter : DataFilter
{
    public TimeDomain TimeDomain { get; set; }
    public FastFourierTransformType FastFourierTransform { get; set; }
    public int? LowPass { get; set; }
    public int? HighPass { get; set; }
    public bool NoRms { get; set; }
    public FastFourierTransformWindow FastFourierTransformWindow { get; set; }
    public double? FastFourierTransformFrom { get; set; }
    public double? FastFourierTransformTo { get; set; }

    public bool LinearFrequencyScale { get; set; }
    public bool LogarithmicScale { get; set; }
    public int? Range { get; set; }
    public string? XLabel { get; set; }
    public string? YLabel { get; set; }
    public string? Title { get; set; }
}