using NcvibJson.Continuous.V2_0;
using NcvibJson.Conversion;
using NcvibJson.Conversion.Filter.Continuous;
using NcvibJson.Conversion.Filter.Triggered;
using NcvibJson.Triggered.V2_0;

namespace MockConverter;

public class NegateSamplesConverter : IConverter
{
    public ContinuousData? Execute(ContinuousDataFilter filter)
    {
        var data = Deserializer.DeserializeFromFile<ContinuousData>(filter.InputFilePath);
        NegateSamples(data);
        
        return data;
    }

    public TriggeredData? Execute(TriggeredDataFilter filter)
    {
        var data = Deserializer.DeserializeFromFile<TriggeredData>(filter.InputFilePath);
        NegateSamples(data);
        
        return data;
    }

    private static void NegateSamples(ContinuousData? data)
    {
        data?.Samples.ForEach(sample=>sample.Values = sample.Values.Select(value=>-value).ToList());
    }

    private static void NegateSamples(TriggeredData? data)
    {
        if (data == null)
        {
            return;
        }
        
        for (var index = 0; index < data.Samples.Count; index++)
        {
            data.Samples[index] = data.Samples[index].Select(value => -value).ToList();
        }
    }
}