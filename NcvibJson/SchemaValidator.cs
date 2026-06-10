using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NJsonSchema;
using NJsonSchema.Validation;
using System.Reflection;

namespace NcvibJson;

public class SchemaValidator : ISchemaValidator
{
    private readonly Dictionary<string, JsonSchema> _schemaCache = new();
    private readonly string _tempSchemasDir;
    private readonly object _lockObject = new();
    private readonly ILogger<SchemaValidator> _logger;

    public SchemaValidator(ILogger<SchemaValidator>? logger = null)
    {
        _logger = logger ?? NullLogger<SchemaValidator>.Instance;

        var assembly = typeof(SchemaValidator).Assembly;
        _tempSchemasDir = Path.Combine(Path.GetTempPath(),$"NcvibJson-Schemas-{assembly.GetName().Version}");

        EnsureSchemaFilesExtracted();
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
                _logger.LogWarning("Schema validation error. Path: {Path}, Kind: {Kind}, Message: {Message}", error.Path, error.Kind, error.ToString());
            }

            return errors.Count == 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating JSON against schema {SchemaPath}", schemaPath);
            return false;
        }
    }

    public bool ValidateJson(string jsonContent, SchemaType schemaType)
    {
        var schemaFilePath = GetSchemaFilePath(schemaType);

        try
        {
            return ValidateJson(jsonContent, schemaFilePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating JSON for schema type {SchemaType}", schemaType);
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
            _logger.LogError(ex, "Error extracting schema resource {ResourceName}", resourceName);
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
            SchemaType.PredefinedFilters => Path.Combine(_tempSchemasDir, "Common", "Standards", "V2_0", "predefined-filters.schema.2.0.json"),
            SchemaType.Axis => Path.Combine(_tempSchemasDir, "Common", "Definitions", "V2_0", "axis.schema.2.0.json"),
            SchemaType.Configuration => Path.Combine(_tempSchemasDir, "Configuration", "V2_0", "configuration.schema.2.0.json"),
            SchemaType.Coordinates => Path.Combine(_tempSchemasDir, "Common", "Definitions", "V2_0", "coordinates.schema.2.0.json"),
            SchemaType.InstrumentDefinition => Path.Combine(_tempSchemasDir, "Common", "Definitions", "V2_0", "instrument-definition.schema.2.0.json"),
            SchemaType.TriggeredDataV1 => Path.Combine(_tempSchemasDir, "Triggered", "V1_0", "triggered-data.schema.1.0.json"),
            _ => throw new ArgumentException($"Unknown schema type: {schemaType}")
        };
    }
}
