using NJsonSchema;
using NJsonSchema.Validation;
using System.Reflection;

namespace NcvibJson;

public class SchemaValidator : ISchemaValidator
{
    private readonly Dictionary<string, JsonSchema> _schemaCache = new();
    private readonly string _tempSchemasDir;
    private readonly object _lockObject = new();
    
    public SchemaValidator()
    {
        var assembly = typeof(SchemaValidator).Assembly;
        _tempSchemasDir = Path.Combine(Path.GetTempPath(),$"NcvibJson-Schemas-{assembly.GetName().Version}");
    }

    public bool ValidateJson(string jsonContent, string schemaPath)
    {
        try
        {
            var schema = GetOrLoadSchema(schemaPath);

            var validator = new JsonSchemaValidator();
            var errors = validator.Validate(jsonContent, schema);

            foreach (var error in errors)
            {
                Console.WriteLine($"Path: {error.Path}, Error: {error.Kind}, Message: {error}");
            }

            return errors.Count == 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error validating JSON: {ex.Message}");
            return false;
        }
    }
    
    public bool ValidateJson(string jsonContent, SchemaType schemaType)
    {
        var schemaFilePath = GetSchemaFilePath(schemaType);
        
        try
        {
            EnsureSchemaFilesExtracted();
            return ValidateJson(jsonContent, schemaFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error validating JSON: {ex.Message}");
            
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner error: {ex.InnerException.Message}");
            }
            
            return false;
        }
    }

    private JsonSchema GetOrLoadSchema(string schemaPath)
    {
        lock (_lockObject)
        {
            if (_schemaCache.TryGetValue(schemaPath, out var cachedSchema))
            {
                return cachedSchema;
            }

            var jsonSchema = File.ReadAllText(schemaPath);
            var schema = JsonSchema.FromJsonAsync(jsonSchema, schemaPath).Result;
            
            _schemaCache[schemaPath] = schema;
            
            return schema;
        }
    }
    
    public void ClearCache()
    {
        lock (_lockObject)
        {
            _schemaCache.Clear();
        }
    }
    
    private void EnsureSchemaFilesExtracted()
    {
        lock (_lockObject)
        {
            Directory.CreateDirectory(_tempSchemasDir);
        
            var assembly = typeof(SchemaValidator).Assembly;
            var assemblyName = assembly.GetName().Name;
        
            foreach (SchemaType schemaType in Enum.GetValues(typeof(SchemaType)))
            {
                var outputPath = GetSchemaFilePath(schemaType);

                if (File.Exists(outputPath))
                {
                    continue;
                }
                
                var relativePath = outputPath.Substring(_tempSchemasDir.Length).TrimStart(Path.DirectorySeparatorChar);
                var resourcePath = relativePath.Replace(Path.DirectorySeparatorChar, '.');
                var resourceName = $"{assemblyName}.{resourcePath}";
                    
                ExtractSchemaResource(assembly, resourceName, outputPath);
            }
        }
    }

    private void ExtractSchemaResource(Assembly assembly, string resourceName, string outputPath)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
            
            using var stream = assembly.GetManifestResourceStream(resourceName);
            
            if (stream == null)
            {
                throw new InvalidOperationException($"Embedded schema resource not found: {resourceName}");
            }
            
            using var fileStream = new FileStream(outputPath, FileMode.Create);
            stream.CopyTo(fileStream);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error extracting schema resource {resourceName}: {ex.Message}");
            throw;
        }
    }
    
    private string GetSchemaFilePath(SchemaType schemaType)
    {
        return schemaType switch
        {
            SchemaType.ContinuousData => Path.Combine(_tempSchemasDir, "Continuous", "V2_0", "continuous-data.schema.2.0.json"),
            SchemaType.TriggeredData => Path.Combine(_tempSchemasDir, "Triggered", "V2_0", "triggered-data.schema.2.0.json"),
            SchemaType.Standards => Path.Combine(_tempSchemasDir, "Common", "Standards", "V2_0", "standards.schema.2.0.json"),
            SchemaType.Axis => Path.Combine(_tempSchemasDir, "Common", "Definitions", "V2_0", "axis.schema.2.0.json"),
            SchemaType.Coordinates => Path.Combine(_tempSchemasDir, "Common", "Definitions", "V2_0", "coordinates.schema.2.0.json"),
            SchemaType.InstrumentDefinition => Path.Combine(_tempSchemasDir, "Common", "Definitions", "V2_0", "instrument-definition.schema.2.0.json"),
            SchemaType.TriggeredDataV1 => Path.Combine(_tempSchemasDir, "Triggered", "V0_1", "triggered-data.schema.1.0.json"),
            _ => throw new ArgumentException($"Unknown schema type: {schemaType}")
        };
    }
}