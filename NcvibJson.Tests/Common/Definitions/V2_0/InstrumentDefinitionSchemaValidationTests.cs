using System.Text.Json;
using NcvibJson.Common.Definitions.V2_0;

namespace NcvibJson.Tests.Common.Definitions.V2_0;

[TestFixture]
public class InstrumentDefinitionSchemaValidationTests
{
    private SchemaValidator _validator;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() {WriteIndented = true};

    [SetUp]
    public void Setup()
    {
        _validator = new SchemaValidator();
    }
    
    [Test]
    public void ValidInstrumentDefinitionJsonShouldPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.True);        
    }
    
    [Test]
    public void InstrumentDefinitionJsonWithWrongVersionShouldNotPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "formatVersion", "1.0");
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.False);        
    }      

    [Test]
    public void InstrumentDefinitionJsonWithNullCompanyShouldNotPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "company", null);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.False);        
    }

    [Test]
    public void InstrumentDefinitionJsonWithoutCompanyShouldNotPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "company", null, true);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.False);        
    }
    
    [Test]
    public void InstrumentDefinitionJsonWithNullTypeShouldNotPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "type", null);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.False);        
    }

    [Test]
    public void InstrumentDefinitionJsonWithoutTypeShouldNotPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "type", null, true);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.False);        
    }    
    
    [Test]
    public void InstrumentDefinitionJsonWithNullSerialNumberShouldNotPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "serialNumber", null);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.False);        
    }

    [Test]
    public void InstrumentDefinitionJsonWithoutSerialNumberShouldNotPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "serialNumber", null, true);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.False);        
    }        

    private InstrumentDefinition CreateBasicInstrumentDefinition()
    {
        return new InstrumentDefinition
        {
            FormatVersion = "2.0",
            Company = "AnyCompany",
            Type = "AnyInstrumentType",
            SerialNumber = 123456
        };
    }
}