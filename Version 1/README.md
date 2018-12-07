# Specification for Ncvib json

Version 1

Filename
UniqueValueForThatCompanyAndSensor_FormatVersion.json  
Example: **abc123_v1.json**
**or companyA_sensorA_1234_2018-01-16T15.26.00+1.00_v1.json** 

Compression
Use GZIP as compression with extension gz.  
Example: **abc123_v1.json.gz**
**or companyA_sensorA_1234_2018-01-16T15.26.00+1.00_v1.json**

Example json
```javascript
{
  // REQUIRED. Ncvib json format version.
  "formatVersion": "1",
  // REQUIRED. name of instrument company.
  "instrumentCompany": "companyA",
  // REQUIRED. name of instrument.
  "instrumentType": "instrumentA",
  // REQUIRED. serial number of instrument.
  "serialNumber": "1234",
  // OPTIONAL. The sensor type if the instruemnt is a logger.
  "sensorType": "sensorA",
  // OPTIONAL. Connection port on the logger, that the sensor is connected to.
  "port": 1,
  // REQUIRED. ISO8601 date/time included time zone for triggered value.
  "time": "2018-01-16T15:26:00+1:00",
  // REQUIRED/OPTIONAL. REQUIRED if time is not specified as a column in samples.
  "sampleRate": "1024",
  // OPTIONAL.  Pretrigger time in samples in seconds. 
  "preTriggerSamples": "1024",
  // OPTIONAL. Pretrigger time in samples in seconds.
  "preTriggerRecordTime" : 1,
  // REQUIRED/OPTIONAL. REQUIRED if multiple column in values
  // If multiple axis use V, L, T for marking vertical longitudinal transverse axis or x, y, z for sensor internal.
  "axes": ["V", "L", "T"],
  // OPTIONAL. Transformation matrix, array the 3x3 matrix for converting sensor internal x,y,z to world V, L, T.
  "transformationMatrix" : [1, 0 , 0, 0, 1, 0, 0, 0, 1],
  // OPTIONAL. Specifies the trigger level per axis.
  "triggerLevel": [10, 10, 10],
  // OPTIONAL. Specifies the trigger level unit
  "triggerLevelUnit": "mm/s"
  // OPTIONAL. unit, entity and values in each axis excluded time column if present
  "values": [
    { 
      "value": [10, 10, 10],
      "unit": "mm/s",
      "entity": "Velocity",
      "timeOffsetInSeconds": "1.2"
    },
    { 
      "value": [10, 10, 10],
      "unit": "m/s2",
      "entity": "Acceleration"
    },
    { 
      "value": [10, 10, 10],
      "unit": "um",
      "entity": "Displacement"
    }
    {
      "value": [10, 10, 10],
      "unit": "um",
      "entity": "ZeroCuttingFrequencyVelocity"
    }
  ],
  // OPTIONAL. filter profile name used by the instrument
  "filterProfile" : "blasting",
  // OPTIONAL. Defined of the filer
  "filterDefinition": {
    "highPassFrequency": "5",
    "highPassOrder": "2",
    "lowPassFrequency": "350",
    "lowPassOrder": "3",
    "family": "butterworth"
  },
  // OPTIONAL. latitude, longitude, altitude in WGS84. Altiude default 0 if only array with lat, long.
  "coordinate": [59.329444, 18.068611, -32.2],
  // OPTIONAL. 
  "battery": "12.76",
  // OPTIONAL. 
  "temperature": "20.0",
  // OPTIONAL. Defalut false, true if has overloaded samples
  "overload":false,
  // REQUIRED. The physical quantity measured in samples
  "sampleEntity": "Velocity",
  // REQUIRED. unit for each column in data
  "sampleUnits": ["s", "m/s2","m/s2","m/s2"],
  // OPTIONAL. The source of the sample, raw or filtered.
  "sampleSource": "raw"
  // REQUIRED.
  "samples": [
    [-1, -1, -1, -1],
    [0, 1.0, 1.0, 1.0],
    [0.00098, 1.1, 1.1, 1.1],
    [10, 10.0, 10.0, 10.0],
  ]
}
```


**Valid entities:**
* Acceleration
* Velocity
* Displacement
* ZeroCuttingFrequencyVelocity
* ZeroCuttingFrequencyAcceleration
* SoundLevelMax
* SoundLevelAverage
* VibrationLevelMax
* VibrationLevelTop10
* Velocity RMS
* Voltage
