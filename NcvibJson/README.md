# Specification for NcvibJson

NcvibJson is essentially a group of schemas for integration of vibration sensors, consisting of schemas for the following:
* Transient values
* Continuous values
* Instrument configuration (to instrument)
* Instrument status (from Instrument)

There are also some common sub-schemas used to not repeat reusable structures:
* Axis - V or Vertical, L or Longitudinal, T or Transversal
* Coordinates - Longitude, Latitude and optional Elevation
* Instrument-Definition - Supplier Company, Instrument Type and SerialNumber
* Standards - A list of standards with Name + High Pass and Low Pass frequencies 

> **Use V2.0 for all new integrations.** See the [repository root README](../README.md) for the
> current spec, a valid example, and the most common producer mistakes.

## Transient/Triggered schema history
* **V2_0 — current.** Use this for all new integrations.
* V1_0 — **DEPRECATED**. Initial version communicated outwards, but never really in use.
* V0_1 — **DEPRECATED**. Schema-less version used in NCVIB production until summer 2025. Also exists in NCVIB.Shared.


## Continuous/Interval schema history
* **V2_0 — current.** Initial version for continuous data (interval values).



Developer note: Remember to add schema type to enum SchemaType if a new schema is created
