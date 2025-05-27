using NJsonSchema;
using NJsonSchema.Validation;

namespace NcvibJson;

public class SchemaValidator
{
    private static bool ValidateJson(string jsonContent, string schemaPath)
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
}