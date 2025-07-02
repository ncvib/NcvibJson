using System.Text.Json;
using NcvibJson.Common.Standards.V2_0;

namespace NcvibJson.Tests.Common.Standards.V2_0;

[TestFixture]
public class StandardsSchemaValidationTests
{
    private SchemaValidator _validator;
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() {WriteIndented = true };

    [SetUp]
    public void Setup()
    {
        _validator = new SchemaValidator();
    }
    
    [Test]
    public void ValidStandardJsonShouldPassValidation()
    {
        var standardFilter = CreateBasicStandardFilter();
        var serialized = JsonSerializer.Serialize(standardFilter, options: _jsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.Standards);
        
        Assert.That(validationResult, Is.True);        
    }

    [Test]
    public void InvalidLowPassFor_DIN_4150_3_ShouldFailValidation()
    {
        var standardFilter = CreateBasicStandardFilter(lowPass: 12345);
        var serialized = JsonSerializer.Serialize(standardFilter, options: _jsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.Standards);
        
        Assert.That(validationResult, Is.False);        
    }

    [Test]
    public void MissingFilterDefinitionShouldFailValidation()
    {
        var standardFilter = CreateBasicStandardFilter();
        var serialized = JsonSerializer.Serialize(standardFilter, options: _jsonSerializerOptions);
        serialized = TestJsonHelper.ReplaceValue(serialized, "filterDefinition", null, true);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.Standards);
        
        Assert.That(validationResult, Is.False);        
    }

    [Test]
    public void UnknownStandardNameShouldFailValidation()
    {
        var standardFilter = CreateBasicStandardFilter(name:"NON_EXISTENT_STANDARD");
        var serialized = JsonSerializer.Serialize(standardFilter, options: _jsonSerializerOptions);
        Console.WriteLine(serialized);
        
        var validationResult = _validator.ValidateJson(serialized, SchemaType.Standards);
        
        Assert.That(validationResult, Is.False);       
    }

    [Test]
    public void ValidateAllPredefinedStandards()
    {
        var filterTypes = 
            Enum
                .GetValues(typeof(PredefinedFilterType))
                .Cast<PredefinedFilterType>();
    
        foreach (var filterType in filterTypes)
        {
            var filter = PredefinedFilters.GetFilter(filterType);
            var standardName = filter.Name;
            var lowPass = filter.FilterDefinition.LowPass;
            var highPass = filter.FilterDefinition.HighPass;
            
            var standardFilter = CreateBasicStandardFilter(name: standardName, lowPass: lowPass, highPass: highPass);
            var serialized = JsonSerializer.Serialize(standardFilter, options: _jsonSerializerOptions);
            Console.WriteLine(serialized);
        
            var validationResult = _validator.ValidateJson(serialized, SchemaType.Standards);
        
            Assert.That(validationResult, Is.True);   
        }
    }

    private StandardFilter CreateBasicStandardFilter(string name = "DIN 4150-3", double lowPass = 1, double highPass = 315)
    {
        return new StandardFilter
        {
            Name = name,
            FilterDefinition = new FilterDefinition
            {
                LowPass = lowPass,
                HighPass = highPass
            }
        };
    }
}