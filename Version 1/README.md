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
  // REQUIRED. Ncvib json format version
  "formatVersion": "1",
  // REQUIRED. name of sensor company
  "sensorCompany": "companyA",
  // REQUIRED. name of sensor
  "sensorType": "sensorA",
  // REQUIRED. serial number of sensor
  "serialNumber": "1234",
  // OPTIONAL. Connection port on the logger, that the sensor is connected to 
  "port": 1,
  // REQUIRED. ISO8601 date/time included time zone for triggered value.
  "time": "2018-01-16T15:26:00+1:00",
  // REQUIRED/OPTIONAL. REQUIRED if time is not specified as a column in samples.
  "sampleRate": "1024",
  // REQUIRED/OPTIONAL. REQUIRED if time is not specified as a column in samples.
  **"PreTrigger": "1024",**
  // REQUIRED/OPTIONAL. REQUIRED if multiple column in values
  // If multiple axis use V, L, T for marking vertical longitudinal transverse axis or x, y, z for sensor internal.
  "Axes": ["V", "L", "T"],
  // OPTIONAL. Transformation matrix, array the 3x3 matrix for converting sensor internal x,y,z to world V, L, T.
  "transformationMatrix" : [1, 0 , 0, 0, 1, 0, 0, 0, 1],
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
  // OPTIONAL. Defined by name or high pass/low pass the filter settings for calculated Values.
  "Filter": {
    "name": "Blasting"
    "highPassFrequency": "5",
    "highPassorder": "2",
    "lowPassFrequency": "5",
    "lowPassorder": "2",    
    "note": ""
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
  // REQUIRED/OPTIONAL. REQUIRED if samples are not filtered as values 
  "sampleFilter": {
    "name": "Raw"
  },
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
