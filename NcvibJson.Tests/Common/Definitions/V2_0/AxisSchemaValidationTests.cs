using System.Text.Json;

namespace NcvibJson.Tests.Common.Definitions.V2_0;

[TestFixture]
public class AxisSchemaValidationTests
{
    protected SchemaValidator Validator;
    protected readonly JsonSerializerOptions JsonSerializerOptions = new() {WriteIndented = true };
    
    [SetUp]
    public void Setup()
    {
        Validator = new SchemaValidator();
    }
    
    [Test]
    [TestCase("Vertical")]
    [TestCase("Longitudinal")]
    [TestCase("Transversal")]
    [TestCase("V")]
    [TestCase("L")]
    [TestCase("T")]
    public void ValidAxisJsonShouldPassValidation(string axis)
    {
        var serialized = JsonSerializer.Serialize(axis, options: JsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = Validator.ValidateJson(serialized, SchemaType.Axis);
        
        Assert.That(validationResult, Is.True);        
    }
    
    [Test]
    [TestCase("X")]
    [TestCase("Y")]
    [TestCase("Z")]
    [TestCase("Gurka")]
    public void InValidAxisJsonShouldNotPassValidation(string axis)
    {
        var serialized = JsonSerializer.Serialize(axis, options: JsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = Validator.ValidateJson(serialized, SchemaType.Axis);
        
        Assert.That(validationResult, Is.False);        
    }    
}