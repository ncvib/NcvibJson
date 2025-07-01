# Specification for NcvibJson

NcvibJson is essentially a group of schemas for integration of vibration sensors, consisting of schemas for the following:
* Transient values
* Continuous values
* Instrument configuration => To instrument
* Instrument status => From Instrument

There are also some common sub-schemas used to not repeat reusable structures:
* Axis - V or Vertical, L or Longitudinal, T or Transversal
* Coordinates - Longitude, Latitude and optional Elevation
* Instrument-Definition - Supplier Company, Instrument Type and SerialNumber
* Standards - A list of standards with Name + High Pass and Low Pass frequencies 

## Transient/Triggered schema history
* V0_1 is a version not using schema used in NCVIB production until summer 2025. Also exists in NCVIB.Shared.
* V1_0 is the initial version communicated outwards, but never really in use
* V2_0 is the latest version. Still in preview.

## Continuous/Interval schema history
* V2_0 initial version for continuous data (interval values). Still in preview.



Developer note: Remember to add schema type to enum SchemaType if a new schema is created