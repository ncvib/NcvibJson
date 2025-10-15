using System.Text;
using System.Text.Json;
using NcvibJson.Common.Definitions.V2_0;
using NcvibJson.Continuous.V2_0;
using NcvibJson.Conversion;
using NcvibJson.Conversion.Filter.Continuous;

namespace MockConverter.Tests.ContinuousDataConversion;

public abstract class BaseContext
{
    public required string InputFilePath;
    public required ContinuousDataFilter Filter;
    public required IConverter Converter;
    
    [TearDown]
    public void TearDown()
    {
        if (File.Exists(InputFilePath))
        {
            File.Delete(InputFilePath);
        }
    }

    protected void GivenAConverter(IConverter converter)
    {
        Converter = converter;
    }    
    
    protected void GivenAContinuousDataInputFile()
    {
        var continuousData = new ContinuousData
        {
            FormatVersion = "2.0",
            InstrumentDefinition = new InstrumentDefinition
            {
                FormatVersion = "2.0",
                Company = "AnyCompany",
                SensorType = "AnyInstrumentType",
                SensorSerialNumber = 123456
            },
            StartTime = DateTime.Today,
            Port = 0,
            IntervalTimeInSeconds = 1,
            Quantity = "Velocity",
            Unit = "mm/s",
            Axes = [Axis.Vertical, Axis.Longitudinal, Axis.Transversal],
            Samples = [
                new ContinuousData.Sample{Time = "2025-10-09T00:00:00", Values = [1,2,3]},
                new ContinuousData.Sample{Time = "2025-10-09T00:01:00", Values = [4,5,6]},
                new ContinuousData.Sample{Time = "2025-10-09T00:02:00", Values = [7,8,9]}
            ]
        };
        
        var json = JsonSerializer.Serialize(continuousData);
        InputFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
        File.WriteAllText(InputFilePath, json, Encoding.UTF8);
    }

    protected void GivenAContinuousDataFilter(string filePath)
    {
        Filter = new ContinuousDataFilter
        {
            LicenseKey = "a valid key",
            InputFilePath = filePath
        };
    }
}