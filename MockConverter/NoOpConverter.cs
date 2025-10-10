using NcvibJson.Continuous.V2_0;
using NcvibJson.Conversion;
using NcvibJson.Conversion.Filter;
using NcvibJson.Conversion.Filter.Continuous;
using NcvibJson.Conversion.Filter.Triggered;
using NcvibJson.Triggered.V2_0;

namespace MockConverter;

public class NoOpConverter : IConverter
{
    public CommonConverterCapabilities CommonConverterCapabilities => CommonConverterCapabilities.None;
    public TransientConverterCapabilities TransientConverterCapabilities => TransientConverterCapabilities.None;
    
    public ContinuousData? Execute(ContinuousDataFilter filter)
    {
        return Deserializer.DeserializeFromFile<ContinuousData>(filter.InputFilePath);
    }

    public TriggeredData? Execute(TriggeredDataFilter filter)
    {
        return Deserializer.DeserializeFromFile<TriggeredData>(filter.InputFilePath);
    }
}