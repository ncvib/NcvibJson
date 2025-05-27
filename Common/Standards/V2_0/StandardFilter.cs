using System.Text.Json.Serialization;

namespace NcvibJson.Common.Standards.V2_0;

public class StandardFilter
{
    [JsonPropertyName("name")] public required string Name { get; set; }
    [JsonPropertyName("filterDefinition")] public required FilterDefinition FilterDefinition { get; set; }

    public StandardFilter(PredefinedFilterType filterType)
    {
        var predefinedFilter = PredefinedFilters.GetFilter(filterType);
        Name = predefinedFilter.Name;
        FilterDefinition = predefinedFilter.FilterDefinition;
    }

    // Empty constructor for deserialization
    public StandardFilter()
    {
    }
}