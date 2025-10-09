using System.Text.Json;
using NcvibJson;

namespace MockConverter.Tests.ContinuousDataConversion.NewStructureConversion;

[TestFixture]
public class SunnyDayScenario : BaseContext
{
    [SetUp]
    public void Setup()
    {
        GivenAConverter(new NewStructureConverter());
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
    public void QuickVerificationThatUnitIsConvertedToUpperCase()
    {
        var result = Converter.Execute(Filter);
        
        Assert.That(result?.Unit, Is.EqualTo("MM/S"));
        Assert.That(result?.Unit, Is.Not.EqualTo("mm/s"));
    }
}