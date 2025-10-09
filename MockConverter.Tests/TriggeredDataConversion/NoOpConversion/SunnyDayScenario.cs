using System.Text.Json;
using NcvibJson;

namespace MockConverter.Tests.TriggeredDataConversion.NoOpConversion;

[TestFixture]
public class SunnyDayScenario : BaseContext
{
    [SetUp]
    public void Setup()
    {
        GivenAConverter(new NoOpConverter());
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
}