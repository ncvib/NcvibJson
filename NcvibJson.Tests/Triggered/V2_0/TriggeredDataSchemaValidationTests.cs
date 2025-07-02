using System.Text.Json;
using NcvibJson.Common.Definitions.V2_0;
using NcvibJson.Common.Standards.V2_0;
using NcvibJson.Triggered.V2_0;

namespace NcvibJson.Tests.Triggered.V2_0;

[TestFixture]
public class TriggeredDataSchemaValidationTests
{
    private SchemaValidator _validator;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() {WriteIndented = true, MaxDepth = 5, };

    [SetUp]
    public void Setup()
    {
        _validator = new SchemaValidator();
    }

    [Test]
    public void ValidTriggeredData_ShouldPass()
    {
        var triggeredData = CreateBasicTriggeredData();
        var serialized = JsonSerializer.Serialize(triggeredData, options: _jsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.TriggeredData);
        
        Assert.That(validationResult, Is.True);
    }

    [Test]
    public void ValidTriggeredData_WithStandard_ShouldPass()
    {
        var triggeredData = CreateBasicTriggeredData();
        triggeredData.Standard = PredefinedFilters.GetFilter(PredefinedFilterType.DIN41503);
        var serialized = JsonSerializer.Serialize(triggeredData, options: _jsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.TriggeredData);
        
        Assert.That(validationResult, Is.True);
    }

    [Test]
    public void ValidTriggeredData_WithStandardEnum_ShouldPass()
    {
        var triggeredData = CreateBasicTriggeredData();
        var serialized = JsonSerializer.Serialize(triggeredData, options: _jsonSerializerOptions);
        var modifiedJson = TestJsonHelper.ReplaceValue(serialized, "standard", "DIN41503");
        Console.WriteLine(modifiedJson);
    
        var validationResult = _validator.ValidateJson(modifiedJson, SchemaType.TriggeredData);
    
        Assert.That(validationResult, Is.True);
    }
    
    [Test]
    public void ValidTriggeredData_WithMissingStandardEnum_ShouldNotPass()
    {
        var triggeredData = CreateBasicTriggeredData();
        var serialized = JsonSerializer.Serialize(triggeredData, options: _jsonSerializerOptions);
        var modifiedJson = TestJsonHelper.ReplaceValue(serialized, "standard", "NOT_EXISTING_STANDARD");
        Console.WriteLine(modifiedJson);
    
        var validationResult = _validator.ValidateJson(modifiedJson, SchemaType.TriggeredData);
    
        Assert.That(validationResult, Is.False);
    }

    [Test]
    public void TriggeredData_WithOptionalElevation_ShouldPass()
    {
        var triggeredData = CreateBasicTriggeredData();
        triggeredData.Coordinates = new Coordinates
        {
            Latitude = 59.123456,
            Longitude = 18.123456,
            ElevationInMeters = 10.5
        };
        var serialized = JsonSerializer.Serialize(triggeredData, options: _jsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.TriggeredData);
        
        Assert.That(validationResult, Is.True);     
    }
    
    [Test]
    public void TriggeredData_WithoutOptionalElevation_ShouldPass()
    {
        var triggeredData = CreateBasicTriggeredData();
        triggeredData.Coordinates = new Coordinates
        {
            Latitude = 59.123456,
            Longitude = 18.123456
        };
        var serialized = JsonSerializer.Serialize(triggeredData, options: _jsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.TriggeredData);
        
        Assert.That(validationResult, Is.True);     
    }    

    [Test]
    public void TriggeredData_WithoutMeasuredQuantity_ShouldFail()
    {
        var triggeredData = CreateBasicTriggeredData();
        triggeredData.MeasuredQuantity = TriggeredData.MeasuredQuantityType.Velocity;
        var serialized = JsonSerializer.Serialize(triggeredData, options: _jsonSerializerOptions);
        var modifiedJson = TestJsonHelper.ReplaceValue(serialized, "measuredQuantity", null, true);
        Console.WriteLine(modifiedJson);        
        
        var validationResult = _validator.ValidateJson(modifiedJson, SchemaType.TriggeredData);
        
        Assert.That(validationResult, Is.False);     
    }

    [Test]
    public void TriggeredData_WithInvalidFormatVersion_ShouldFail()
    {
        var triggeredData = CreateBasicTriggeredData();
        triggeredData.FormatVersion = "1.0";
        var serialized = JsonSerializer.Serialize(triggeredData, options: _jsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.TriggeredData);
        
        Assert.That(validationResult, Is.False);         
    }

    [Test]
    public void TriggeredData_WithInvalidMeasuredQuantity_ShouldFail()
    {
        var triggeredData = CreateBasicTriggeredData();
        triggeredData.MeasuredQuantity = TriggeredData.MeasuredQuantityType.Velocity;
        var serialized = JsonSerializer.Serialize(triggeredData, options: _jsonSerializerOptions);
        var modifiedJson = TestJsonHelper.ReplaceValue(serialized, "measuredQuantity", "InvalidQuantity");
        Console.WriteLine(modifiedJson);        
        
        var validationResult = _validator.ValidateJson(modifiedJson, SchemaType.TriggeredData);
        
        Assert.That(validationResult, Is.False);             
    }

    [Test]
    [TestCase("Velocity")]
    [TestCase("Acceleration")]
    [TestCase("Displacement")]
    [TestCase("Voltage")]
    [TestCase("SoundPressure")]
    [TestCase("SoundIntensity")]
    public void TriggeredData_ValidMeasuredQuantities_ShouldPass(string quantity)
    {
        var triggeredData = CreateBasicTriggeredData();
        triggeredData.MeasuredQuantity = TriggeredData.MeasuredQuantityType.Velocity;
        var serialized = JsonSerializer.Serialize(triggeredData, options: _jsonSerializerOptions);
        var modifiedJson = TestJsonHelper.ReplaceValue(serialized, "measuredQuantity", quantity);
        Console.WriteLine(modifiedJson);        
        
        var validationResult = _validator.ValidateJson(modifiedJson, SchemaType.TriggeredData);
        
        Assert.That(validationResult, Is.True);      
    }

    private static TriggeredData CreateBasicTriggeredData()
    {
        var triggeredData = new TriggeredData
        {
            FormatVersion = "2.0",
            InstrumentDefinition = new InstrumentDefinition
            {
                FormatVersion = "2.0",
                Company = "AnyCompany",
                Type = "AnyInstrumentType",
                SerialNumber = 123456
            },
            MeasuredQuantity = TriggeredData.MeasuredQuantityType.Velocity,
            MeasuredUnit = "mm/s",
            StartTime = DateTime.Today,
            SampleRate = 4000,
            SampleUnits = [$"{{{string.Join(",", Enumerable.Repeat("mm/s", 3))}}}"],
            Samples = [new List<double> {0.0, 0.0, 0.0}, new List<double> {1.0, 1.0, 1.0}, new List<double> {2.0, 2.0, 2.0}]
        };
        return triggeredData;
    }
}