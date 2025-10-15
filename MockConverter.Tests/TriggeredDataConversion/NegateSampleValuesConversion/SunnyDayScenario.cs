using System.Text.Json;
using NcvibJson;
using NcvibJson.Triggered.V2_0;

namespace MockConverter.Tests.TriggeredDataConversion.NegateSampleValuesConversion;

[TestFixture]
public class SunnyDayScenario : BaseContext
{
    [SetUp]
    public void Setup()
    {
        GivenAConverter(new NegateSamplesConverter());
        GivenATriggeredDataInputFile();
        GivenATriggeredDataFilter(InputFilePath);
    }

    [Test]
    public void FileCanBeConvertedToTriggeredData()
    {
        var result = Converter.Execute(Filter);
        
        Assert.That(result, Is.Not.Null);
    }
    
    [Test]
    public void ConvertedTriggeredDataIsValid()
    {
        var result = Converter.Execute(Filter);
        
        var validator = new SchemaValidator();
        var isValid = validator.ValidateJson(JsonSerializer.Serialize(result), SchemaType.TriggeredData);

        Assert.That(isValid);
    }    
    
    [Test]
    public void NonConvertedContinuousDataHasPositiveSamples()
    {
        var source = File.ReadAllText(InputFilePath);
        var originalData = JsonSerializer.Deserialize<TriggeredData>(source);
        var allNegative = AllNegative(originalData);

        Assert.That(allNegative, Is.False);        
    } 
    
    [Test]
    public void ConvertedContinuousDataOnlyHasNegativeSampleValues()
    {
        var result = Converter.Execute(Filter);
        var allNegative = AllNegative(result);

        Assert.That(allNegative, Is.True);
    }    
    
    private static bool AllNegative(TriggeredData? data)
    {
        return data?.Samples.Any(s => s.Any(v => v > 0)) == false;
    }    
}