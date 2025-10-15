using System.Text.Json;

namespace MockConverter;

public static class Deserializer
{
    public static T? DeserializeFromFile<T>(string? filePath) where T : class
    {
        if (filePath == null)
        {
            return null;
        }
        
        var source = File.ReadAllText(filePath);
        var data = JsonSerializer.Deserialize<T>(source);
        
        return data;
    }
}