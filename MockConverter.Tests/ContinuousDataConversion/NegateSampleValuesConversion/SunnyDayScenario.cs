using System.Text.Json;
using NcvibJson;
using NcvibJson.Continuous.V2_0;

namespace MockConverter.Tests.ContinuousDataConversion.NegateSampleValuesConversion;

[TestFixture]
public class SunnyDayScenario : BaseContext
{
    [SetUp]
    public void Setup()
    {
        GivenAConverter(new NegateSamplesConverter());
        GivenAContinuousDataInputFile();
        GivenAContinuousDataFilter(InputFilePath);
    }

    [Test]
    public void FileCanBeConvertedToContinuousData()
    {
        var result = Converter.Execute(Filter);
        
        Assert.That(result, Is.Not.Null);
    }
    
    [Test]
    public void ConvertedContinuousDataIsValid()
    {
        var result = Converter.Execute(Filter);
        
        var validator = new SchemaValidator();
        var isValid = validator.ValidateJson(JsonSerializer.Serialize(result), SchemaType.ContinuousData);

        Assert.That(isValid);
    }

    [Test]
    public void NonConvertedContinuousDataHasPositiveSamples()
    {
        var source = File.ReadAllText(InputFilePath);
        var originalData = JsonSerializer.Deserialize<ContinuousData>(source);
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
    
    private static bool AllNegative(ContinuousData? data)
    {
        return data?.Samples.Any(s => s.Values.Any(v => v > 0)) == false;
    }
}