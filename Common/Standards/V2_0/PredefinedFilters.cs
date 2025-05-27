namespace NcvibJson.Common.Standards.V2_0;

public static class PredefinedFilters
{
    private static readonly Dictionary<PredefinedFilterType, StandardFilter> Filters = new Dictionary<PredefinedFilterType, StandardFilter>
    {
        {
            PredefinedFilterType.Unknown, new StandardFilter
            {
                Name = "Unknown",
                FilterDefinition = new FilterDefinition {LowPass = 0, HighPass = 1000}
            }
        },
        {
            PredefinedFilterType.ISEESeismograph, new StandardFilter
            {
                Name = "ISEE Seismograph",
                FilterDefinition = new FilterDefinition {LowPass = 2, HighPass = 250}
            }
        },
        {
            PredefinedFilterType.DIN41503, new StandardFilter
            {
                Name = "DIN 4150-3",
                FilterDefinition = new FilterDefinition {LowPass = 1, HighPass = 315}
            }
        },
        {
            PredefinedFilterType.DIN41502KB, new StandardFilter
            {
                Name = "DIN 4150-2 KB",
                FilterDefinition = new FilterDefinition {LowPass = 1, HighPass = 80}
            }
        },
        {
            PredefinedFilterType.BS7385, new StandardFilter
            {
                Name = "BS 7385",
                FilterDefinition = new FilterDefinition {LowPass = 1, HighPass = 300}
            }
        },
        {
            PredefinedFilterType.AS21872_2006, new StandardFilter
            {
                Name = "AS 2187-2 2006",
                FilterDefinition = new FilterDefinition {LowPass = 2, HighPass = 250}
            }
        },
        {
            PredefinedFilterType.NORMS9012, new StandardFilter
            {
                Name = "?NORM S 9012",
                FilterDefinition = new FilterDefinition {LowPass = 1, HighPass = 80}
            }
        },
        {
            PredefinedFilterType.ISO8569Accel, new StandardFilter
            {
                Name = "ISO 8569 Accel",
                FilterDefinition = new FilterDefinition {LowPass = 5, HighPass = 300}
            }
        },
        {
            PredefinedFilterType.IN1226, new StandardFilter
            {
                Name = "IN1226",
                FilterDefinition = new FilterDefinition {LowPass = 1, HighPass = 15}
            }
        },
        {
            PredefinedFilterType.NS8176Komfort, new StandardFilter
            {
                Name = "NS 8176 Komfort",
                FilterDefinition = new FilterDefinition {LowPass = 1, HighPass = 80}
            }
        },
        {
            PredefinedFilterType.NS8141Byggverk, new StandardFilter
            {
                Name = "NS 8141 Byggverk",
                FilterDefinition = new FilterDefinition {LowPass = 5, HighPass = 300}
            }
        },
        {
            PredefinedFilterType.NS8141_1_2012_A1_2013, new StandardFilter
            {
                Name = "NS 8141-1:2012 + A1:2013",
                FilterDefinition = new FilterDefinition {LowPass = 3, HighPass = 400}
            }
        },
        {
            PredefinedFilterType.SS4604866Sprang, new StandardFilter
            {
                Name = "SS 4604866 Spräng",
                FilterDefinition = new FilterDefinition {LowPass = 5, HighPass = 312}
            }
        },
        {
            PredefinedFilterType.SS025211Schakt, new StandardFilter
            {
                Name = "SS 025211 Schakt",
                FilterDefinition = new FilterDefinition {LowPass = 2, HighPass = 150}
            }
        },
        {
            PredefinedFilterType.SS4604861Komfort, new StandardFilter
            {
                Name = "SS 4604861 Komfort",
                FilterDefinition = new FilterDefinition {LowPass = 1, HighPass = 80}
            }
        },
        {
            PredefinedFilterType.Geophone, new StandardFilter
            {
                Name = "Geophone",
                FilterDefinition = new FilterDefinition {LowPass = 5, HighPass = 500}
            }
        },
        {
            PredefinedFilterType.ICPECirculaire86, new StandardFilter
            {
                Name = "ICPE-Cirkulaire 86",
                FilterDefinition = new FilterDefinition {LowPass = 1, HighPass = 150}
            }
        },
        {
            PredefinedFilterType.SS25211Schakt5_150Hz, new StandardFilter
            {
                Name = "SS 25211 Schakt 5-150Hz",
                FilterDefinition = new FilterDefinition {LowPass = 5, HighPass = 150}
            }
        },
    };

    public static StandardFilter GetFilter(PredefinedFilterType filterType)
    {
        if (Filters.TryGetValue(filterType, out var filter))
        {
            return filter;
        }

        throw new ArgumentException($"Unknown filter type: {filterType}");
    }

    public static bool TryGetFilterType(string filterName, out PredefinedFilterType filterType)
    {
        var keyValuePair = Filters.FirstOrDefault(f => f.Value.Name == filterName);

        if (keyValuePair.Value != null)
        {
            filterType = keyValuePair.Key;
            return true;
        }

        filterType = default;
        return false;
    }
}