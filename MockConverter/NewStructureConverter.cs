using NcvibJson.Common.Definitions.V2_0;
using NcvibJson.Continuous.V2_0;
using NcvibJson.Conversion;
using NcvibJson.Conversion.Filter.Continuous;
using NcvibJson.Conversion.Filter.Triggered;
using NcvibJson.Triggered.V2_0;

namespace MockConverter;

public class NewStructureConverter : IConverter
{
    public ContinuousData? Execute(ContinuousDataFilter filter)
    {
        var source = ReadContinuousInputFile(filter.InputFilePath!);

        if (source == null)
        {
            return null;       
        }
        
        var target = new ContinuousData
        {
            FormatVersion = "2.0",
            InstrumentDefinition = new InstrumentDefinition
            {
                FormatVersion = "2.0",
                Company = source.InstrumentDefinition.Company.ToUpper(),
                SensorType = source.InstrumentDefinition.SensorType.ToUpper(),
                SensorSerialNumber = 0
            },
            Port = 0,
            StartTime = source.StartTime,
            IntervalTimeInSeconds = source.IntervalTimeInSeconds,
            Quantity = source.Quantity.ToUpper(),
            Unit = source.Unit.ToUpper(),
            Samples = source.Samples
        };
        
        return target;
    }

    public TriggeredData? Execute(TriggeredDataFilter filter)
    {
        var source = ReadTriggeredInputFile(filter.InputFilePath!);

        if (source == null)
        {
            return null;       
        }

        var target = new TriggeredData
        {
            FormatVersion = "2.0",
            HashId = source.HashId,
            InstrumentDefinition = source.InstrumentDefinition,
            MeasuredQuantity = source.MeasuredQuantity,
            MeasuredUnit = source.MeasuredUnit.ToUpper(),
            StartTime = source.StartTime,
            SampleRate = source.SampleRate,
            SampleUnits = source.SampleUnits,
            Samples = source.Samples
        };
            
        return target; 
    }

    private static ContinuousData? ReadContinuousInputFile(string inputFilePath)
    {
        return Deserializer.DeserializeFromFile<ContinuousData>(inputFilePath);
    }

    private TriggeredData? ReadTriggeredInputFile(string inputFilePath)
    {
        return Deserializer.DeserializeFromFile<TriggeredData>(inputFilePath);
    }
}