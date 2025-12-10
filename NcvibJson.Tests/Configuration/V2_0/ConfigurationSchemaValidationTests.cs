using System.Text.Json;
using NcvibJson.Common.Standards.V2_0;

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
    public void ConfigurationJsonWithNodeHavingPredefinedFilterNameShouldPassValidation()
    {
        var filterTypes = 
            Enum
                .GetValues(typeof(PredefinedFilterType))
                .Cast<PredefinedFilterType>();

        foreach (var filterType in filterTypes)
        {
            var filter = PredefinedFilters.GetFilter(filterType);
            
            var configuration = CreateBasicConfiguration();
            var node = new NcvibJson.Configuration.V2_0.Configuration.NodeConfiguration
            {
                Standard = filter
            };
            
            configuration.NodeConfigurations = [node];

            for (var index = 0; index < configuration.NodeConfigurations.Length; index++)
            {
                var serialized = JsonSerializer.Serialize(configuration, options: JsonSerializerOptions);
                serialized = TestJsonHelper.ReplaceValue(serialized, $"nodeConfigurations[{index}].standard", filterType.ToString());
                Console.WriteLine(serialized);

                var validationResult = Validator.ValidateJson(serialized, SchemaType.Configuration);

                Assert.That(validationResult, Is.True);
            }
        }
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
    
    [Test]
    public void ConfigurationJsonWithPretriggeredValueShouldPassValidation()
    {
        var configuration = CreateBasicConfigurationWithNode(preTrigger: 2);
        var serialized = JsonSerializer.Serialize(configuration, options: JsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = Validator.ValidateJson(serialized, SchemaType.Configuration);
        
        Assert.That(validationResult, Is.True);        
    }
    
    [Test]
    public void ConfigurationJsonWithNullPretriggeredValueShouldPassValidation()
    {
        var configuration = CreateBasicConfigurationWithNode(preTrigger: null);
        var serialized = JsonSerializer.Serialize(configuration, options: JsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = Validator.ValidateJson(serialized, SchemaType.Configuration);
        
        Assert.That(validationResult, Is.True);        
    } 
    
    [Test]
    public void ConfigurationJsonWithoutPretriggeredValueShouldPassValidation()
    {
        var configuration = CreateBasicConfigurationWithNode(preTrigger: null);
        var serialized = JsonSerializer.Serialize(configuration, options: JsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "nodeConfigurations[0].preTriggerPeriodInSeconds", null, true);
        Console.WriteLine(serialized);
        
        var validationResult = Validator.ValidateJson(serialized, SchemaType.Configuration);
        
        Assert.That(validationResult, Is.True);        
    } 
}