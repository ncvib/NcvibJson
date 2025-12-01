A converter transforms proprietary instrument data into NCVIB’s generic `ContinuousData` and `TriggeredData` formats.  
Each converter must support both continuous and transient execution paths and may optionally expose additional capabilities (FFT, filtering, formatting overrides).

Converters are used in two scenarios:

1. **Import pipeline** – original files are parsed into NCVIB data structures (no capabilities used).
2. **Post-processing** – the application requests modified output (FFT, filtering, time-domain transforms, etc.), depending on supported capabilities.

---

## Capabilities vs Filters

**Capabilities** define what the converter *supports* (static).  
**Filters** define what the caller *requests* at runtime (dynamic).

Example capability declaration:

```csharp
public TransientConverterCapabilities TransientConverterCapabilities =>
    TransientConverterCapabilities.FastFourierTransformation |
    TransientConverterCapabilities.LowPassFiltering;
````

Example filter usage:

```csharp
var filter = new TriggeredDataFilter
{
    InputFilePath = "data.json",
    UseFft = true,
    UseLowPassFilter = true,
    LowPassFilterFrequency = 1000
};
```

The converter must only apply features that:

1. Are supported (capability flag present)
2. Are explicitly requested via filter properties

---

## Capability Types

### CommonConverterCapabilities

Applies to both continuous and transient processing:

* Formatting overrides
* Time zone handling
* Logging
* File name validation

### TransientConverterCapabilities

Applies only to triggered data:

* FFT and FFT windowing
* Low-/high-pass filtering
* Time-domain switching
* Label/title/range overrides

---

## Minimal Converter (No Capabilities)

```csharp
public class NoOpConverter : IConverter
{
    public CommonConverterCapabilities CommonConverterCapabilities =>
        CommonConverterCapabilities.None;

    public TransientConverterCapabilities TransientConverterCapabilities =>
        TransientConverterCapabilities.None;

    public ContinuousData? Execute(ContinuousDataFilter f) =>
        Deserializer.DeserializeFromFile<ContinuousData>(f.InputFilePath);

    public TriggeredData? Execute(TriggeredDataFilter f) =>
        Deserializer.DeserializeFromFile<TriggeredData>(f.InputFilePath);
}
```

---

## Converter Using Multiple Triggered Capabilities

```csharp
public class AdvancedTriggeredConverter : IConverter
{
    public CommonConverterCapabilities CommonConverterCapabilities =>
        CommonConverterCapabilities.CreateLogFiles;

    public TransientConverterCapabilities TransientConverterCapabilities =>
        TransientConverterCapabilities.FastFourierTransformation |
        TransientConverterCapabilities.LowPassFiltering |
        TransientConverterCapabilities.TimeDomainSwitching;

    public ContinuousData? Execute(ContinuousDataFilter f) =>
        Deserializer.DeserializeFromFile<ContinuousData>(f.InputFilePath);

    public TriggeredData? Execute(TriggeredDataFilter f)
    {
        var data = Deserializer.DeserializeFromFile<TriggeredData>(f.InputFilePath);
        if (data == null) return null;

        if (f.UseLowPassFilter == true)
            data = ApplyLowPass(data, f);

        if (f.UseFft == true)
            data = ApplyFft(data, f);

        if (f.UseTimeDomainSwitch == true)
            data = ApplyTimeDomainSwitch(data, f);

        if (f.CreateLogFile == true)
            WriteLog(data);

        return data;
    }

    private TriggeredData ApplyLowPass(TriggeredData d, TriggeredDataFilter f) => throw new NotImplementedException();
    private TriggeredData ApplyFft(TriggeredData d, TriggeredDataFilter f) => throw new NotImplementedException();
    private TriggeredData ApplyTimeDomainSwitch(TriggeredData d, TriggeredDataFilter f) => throw new NotImplementedException();
    private void WriteLog(TriggeredData d) => throw new NotImplementedException();
}
```

---

## Caller Example

```csharp
IConverter converter = Resolve();

var filter = new TriggeredDataFilter
{
    LicenseKey = "<license>",
    InputFilePath = "input.json",
    UseFft = true,
    UseLowPassFilter = true
};

var result = converter.Execute(filter);
```

---

## Implementation Notes

* Capabilities must accurately reflect supported functionality.
* Filter parameters must never trigger features not declared in capability flags.
* Only apply processing steps when the filter explicitly requests them.
* Keep capability declarations synchronized with actual implementations.

