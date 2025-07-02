using System.Text.Json;

namespace NcvibJson.Tests.Configuration.V2_0;

[TestFixture]
public class ConfigurationSchemaValidationTests : ConfigurationTests
{
    [Test]
    public void ValidConfigurationJsonShouldPassValidation()
    {
        var configuration = CreateBasicConfiguration();
        var serialized = JsonSerializer.Serialize(configuration, options: JsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = Validator.ValidateJson(serialized, SchemaType.Configuration);
        
        Assert.That(validationResult, Is.True);        
    }
    
    [Test]
    public void ConfigurationJsonWithWrongVersionShouldNotPassValidation()
    {
        var configuration = CreateBasicConfiguration();
        configuration.FormatVersion = "1.0";
        var serialized = JsonSerializer.Serialize(configuration, options: JsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = Validator.ValidateJson(serialized, SchemaType.Configuration);
        
        Assert.That(validationResult, Is.False);        
    }

    [Test]
    public void ConfigurationJsonWithNullInstrumentDefinitionShouldNotPassValidation()
    {
        var configuration = CreateBasicConfiguration();
        var serialized = JsonSerializer.Serialize(configuration, options: JsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "instrument", null);
        Console.WriteLine(serialized);
        
        var validationResult = Validator.ValidateJson(serialized, SchemaType.Configuration);
        
        Assert.That(validationResult, Is.False);        
    }

    [Test]
    public void ConfigurationJsonWithoutInstrumentDefinitionShouldNotPassValidation()
    {
        var configuration = CreateBasicConfiguration();
        var serialized = JsonSerializer.Serialize(configuration, options: JsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "instrument", null, true);
        Console.WriteLine(serialized);
        
        var validationResult = Validator.ValidateJson(serialized, SchemaType.Configuration);
        
        Assert.That(validationResult, Is.False);        
    }
}