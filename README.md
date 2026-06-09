# NCVIB JSON

NCVIB JSON (`NcvibJson`) is the data-interchange format for integrating vibration and noise
sensors with the NCVIB system. It is a set of JSON Schemas with matching .NET models, published
as the `NcvibJson` NuGet package.

> ## ➡️ Use **V2.0**
> V2.0 is the current version and is what NCVIB uses in production. The older triggered formats
> **V1.0** and **V0.1** are **deprecated** and kept for reference only — do **not** build new
> integrations against them.

## Versions

| Data                          | Current  | Deprecated   |
|-------------------------------|:--------:|--------------|
| Triggered (transient events)  | **V2.0** | V1.0, V0.1   |
| Continuous (interval values)  | **V2.0** | —            |
| Instrument configuration      | **V2.0** | —            |
| Instrument status             | **V2.0** | —            |

V2.0 schemas live under `NcvibJson/<Family>/V2_0/*.schema.2.0.json`, with shared sub-schemas under
`NcvibJson/Common/` (Axis, Coordinates, Instrument-Definition, Standards).

## Triggered V2.0 — minimal valid example

```json
{
  "formatVersion": "2.0",
  "hashId": "unique-per-event",
  "instrumentDefinition": {
    "formatVersion": "2.0",
    "company": "Acme",
    "sensorType": "Geophone-3C",
    "loggerType": "BMG-Edge",
    "loggerSerialNumber": 555,
    "sensorSerialNumber": 12345678
  },
  "coordinates": { "longitude": 18.06324, "latitude": 59.334591, "elevationInMeters": 28.5 },
  "battery": "87%",
  "temperature": "21.3C",
  "overload": false,
  "filteredSamples": false,
  "standard": { "name": "SS 025211 Schakt", "filterDefinition": { "lowPass": 2, "highPass": 150 } },
  "measuredQuantity": "Velocity",
  "measuredUnit": "mm/s",
  "vectorPeakParticleVelocity": 9.31,
  "timeZone": "Europe/Stockholm",
  "startTime": "2024-09-12T09:14:37.500Z",
  "sampleRate": 1000.0,
  "numberOfPreTriggerSamples": 1,
  "axes": ["Longitudinal", "Transversal", "Vertical"],
  "triggerLevel": [2.0, 2.0, 2.0],
  "triggerLevelUnit": "mm/s",
  "maxSamples": [
    { "axis": "Longitudinal", "quantity": "Velocity", "unit": "mm/s", "value": [4.82], "timeOffsetInSeconds": 0.213 }
  ],
  "sampleUnits": ["mm/s", "mm/s", "mm/s"],
  "samples": [
    [0.01, 4.82, -0.03],
    [0.00, 3.17,  0.01],
    [0.03, 7.54, -0.04]
  ]
}
```

The required fields are `formatVersion`, `hashId`, `instrumentDefinition`, `measuredQuantity`,
`measuredUnit`, `startTime`, `sampleRate`, `sampleUnits`, and `samples`; everything else is optional.
Channels are positional — `axes[i]`, `sampleUnits[i]`, and `samples[i]` all refer to the same channel.

## Common producer mistakes

The points integrators get wrong most often:

- **Axes are orientation _strings_, not objects:** `"axes": ["Longitudinal", "Transversal", "Vertical"]`
  (or `["L", "T", "V"]`; use `N` / `None` for non-directional sensors). The same applies to
  `maxSamples[].axis` — a bare string, e.g. `"axis": "Vertical"`.
- **`sensorSerialNumber` is an integer**, not a string.
- **Instrument fields are `company`, `sensorType`, `sensorSerialNumber`** (plus the required
  `formatVersion`), with optional `loggerType` / `loggerSerialNumber` — not `manufacturer` /
  `model` / `serialNumber`.
- **Coordinates use `longitude`, `latitude`, and optional `elevationInMeters`.**
- **`standard` is a known filter name plus its band:**
  `{ "name": "...", "filterDefinition": { "lowPass": <n>, "highPass": <n> } }`. The recognised
  names and bands are listed in `NcvibJson/Common/Standards/V2_0/`.
- **Omit `measuresSound` / `measuresVibration` / `measuresAnythingButSoundOrVibration`** — NCVIB
  derives these from `measuredQuantity`.

## Validation

Validate your output against the V2.0 schemas before sending. The package also exposes
`SchemaValidator` — e.g. `new SchemaValidator().ValidateJson(json, SchemaType.TriggeredData)`.

---

Developer note: when adding a new schema, remember to add its type to the `SchemaType` enum.
