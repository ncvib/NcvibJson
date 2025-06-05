namespace NcvibJson;

public interface ISchemaValidator
{
    bool ValidateJson(string jsonContent, string schemaPath);
    bool ValidateJson(string jsonContent, SchemaType schemaType);
}