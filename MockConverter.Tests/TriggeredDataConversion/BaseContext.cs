using System.Text;
using System.Text.Json;
using NcvibJson.Common.Definitions.V2_0;
using NcvibJson.Conversion;
using NcvibJson.Conversion.Filter.Triggered;
using NcvibJson.Triggered.V2_0;

namespace MockConverter.Tests.TriggeredDataConversion;

public abstract class BaseContext
{
    public required IConverter Converter;
    public required string InputFilePath;
    public required TriggeredDataFilter Filter;
    
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

    protected void GivenATriggeredDataInputFile()
    {
        var triggeredData = new TriggeredData
        {
            FormatVersion = "2.0",
            HashId = Guid.NewGuid().ToString(),
            InstrumentDefinition = new InstrumentDefinition
            {
                FormatVersion = "2.0",
                Company = "AnyCompany",
                SensorType = "AnyInstrumentType",
                SensorSerialNumber = 123456
            },
            MeasuredQuantity = TriggeredData.MeasuredQuantityType.Velocity,
            MeasuredUnit = "mm/s",
            StartTime = DateTime.Today,
            SampleRate = 4000,
            SampleUnits = [$"{{{string.Join(",", Enumerable.Repeat("mm/s", 3))}}}"],
            Samples = [[0.0, 0.0, 0.0], [1.0, 1.0, 1.0], [2.0, 2.0, 2.0]]
        };
        
        var json = JsonSerializer.Serialize(triggeredData);
        InputFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
        File.WriteAllText(InputFilePath, json, Encoding.UTF8);
    }

    protected void GivenATriggeredDataFilter(string filePath)
    {
        Filter = new TriggeredDataFilter
        {
            LicenseKey = "a valid key",
            InputFilePath = filePath
        };
    }
}