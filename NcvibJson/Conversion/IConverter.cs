using NcvibJson.Continuous.V2_0;
using NcvibJson.Conversion.Filter;
using NcvibJson.Conversion.Filter.Continuous;
using NcvibJson.Conversion.Filter.Triggered;
using NcvibJson.Triggered.V2_0;

namespace NcvibJson.Conversion;

public interface IConverter
{
    CommonConverterCapabilities CommonConverterCapabilities { get; }
    TransientConverterCapabilities TransientConverterCapabilities { get; }
    
    public ContinuousData? Execute(ContinuousDataFilter filter);
    public TriggeredData? Execute(TriggeredDataFilter filter);
}