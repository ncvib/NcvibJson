using NJsonSchema;
using NJsonSchema.Validation;

namespace NcvibJson;

public enum SchemaType
{
    ContinuousData,
    TriggeredData,
    Standards,
    TriggeredDataV1
}

public class SchemaValidator : ISchemaValidator
{
    public bool ValidateJson(string jsonContent, string schemaPath)
    {
        var jsonSchema = File.ReadAllText(schemaPath);
        var schema = JsonSchema.FromJsonAsync(jsonSchema, schemaPath).Result;

        var validator = new JsonSchemaValidator();
        var errors = validator.Validate(jsonContent, schema);

        foreach (var error in errors)
        {
            Console.WriteLine($"Path: {error.Path}, Error: {error.Kind}, Message: {error}");
        }

        return errors.Count == 0;
    }
    
    public bool ValidateJson(string jsonContent, SchemaType schemaType)
    {
        var schemaContent = GetEmbeddedSchema(schemaType);
        var schema = JsonSchema.FromJsonAsync(schemaContent).Result;

        var validator = new JsonSchemaValidator();
        var errors = validator.Validate(jsonContent, schema);

        foreach (var error in errors)
        {
            Console.WriteLine($"Path: {error.Path}, Error: {error.Kind}, Message: {error}");
        }

        return errors.Count == 0;
    }
    
    private string GetEmbeddedSchema(SchemaType schemaType)
    {
        var resourceName = schemaType switch
        {
            SchemaType.ContinuousData => "NcvibJson.Continuous.V2_0.continuous-data.schema.2.0.json",
            SchemaType.TriggeredData => "NcvibJson.Triggered.V2_0.triggered-data.schema.2.0.json",
            SchemaType.Standards => "NcvibJson.Common.Standards.V2_0.standards.schema.2.0.json",
            SchemaType.TriggeredDataV1 => "NcvibJson.Triggered.V0_1.triggered-data.schema.1.0.json",
            _ => throw new ArgumentException($"Unknown schema type: {schemaType}")
        };
        
        var assembly = typeof(SchemaValidator).Assembly;
        using var stream = assembly.GetManifestResourceStream(resourceName);
        
        if (stream == null)
        {
            throw new InvalidOperationException($"Embedded schema resource not found: {resourceName}");
        }
            
        using var reader = new StreamReader(stream);
        
        return reader.ReadToEnd();
    }
}