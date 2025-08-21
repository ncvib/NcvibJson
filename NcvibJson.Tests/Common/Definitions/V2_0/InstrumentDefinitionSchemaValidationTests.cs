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
        serialized = TestJsonHelper.ReplaceValue(serialized, "sensorType", null);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.False);        
    }

    [Test]
    public void InstrumentDefinitionJsonWithoutTypeShouldNotPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "sensorType", null, true);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.False);        
    }    
    
    [Test]
    public void InstrumentDefinitionJsonWithNullLoggerTypeShouldPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "loggerType", null);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.True);        
    }

    [Test]
    public void InstrumentDefinitionJsonWithoutLoggerTypeShouldPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "loggerType", null, true);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.True);        
    }        
    
    [Test]
    public void InstrumentDefinitionJsonWithNullSensorSerialNumberShouldNotPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "sensorSerialNumber", null);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.False);        
    }
    
    [Test]
    public void InstrumentDefinitionJsonWithNullLoggerSerialNumberShouldPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "loggerSerialNumber", null);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.True);        
    }    

    [Test]
    public void InstrumentDefinitionJsonWithoutSensorSerialNumberShouldNotPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "sensorSerialNumber", null, true);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.False);        
    }   
    
    [Test]
    public void InstrumentDefinitionJsonWithoutLoggerSerialNumberShouldPassValidation()
    {
        var instrument = CreateBasicInstrumentDefinition();
        var serialized = JsonSerializer.Serialize(instrument, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "loggerSerialNumber", null, true);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.InstrumentDefinition);
        
        Assert.That(validationResult, Is.True);        
    }       

    private InstrumentDefinition CreateBasicInstrumentDefinition()
    {
        return new InstrumentDefinition
        {
            FormatVersion = "2.0",
            Company = "AnyCompany",
            SensorType = "AnyInstrumentType",
            SensorSerialNumber = 123456
        };
    }
}