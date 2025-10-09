using System.Text.Json;
using NcvibJson;

namespace MockConverter.Tests.TriggeredDataConversion.NewStructureConversion;

[TestFixture]
public class SunnyDayScenario : BaseContext
{
    [SetUp]
    public void Setup()
    {
        GivenAConverter(new NewStructureConverter());
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
    public void QuickVerificationThatUnitIsConvertedToUpperCase()
    {
        var result = Converter.Execute(Filter);
        
        Assert.That(result?.MeasuredUnit, Is.EqualTo("MM/S"));
        Assert.That(result?.MeasuredUnit, Is.Not.EqualTo("mm/s"));
    }
}