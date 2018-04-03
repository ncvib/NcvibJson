# Specification for Ncvib json

Version 1

Filename
CompanyName_SensorType_SerialNumber_Entity_Time_FormatVersion.json  
Example: **companyA_sensorA_1234_Acceleration_2018-01-16T15.26.00+1.00_v1.json**

Compression
Use GZIP as compression with extension gz.  
Example: **companyA_sensorA_1234_Acceleration_2018-01-16T15.26.00+1.00_v1.json.gz**

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
  // REQUIRED. The physical quantity measured by the sensor
  "measuredEntity": "velocity",
  // REQUIRED. The maximal value in this file recorded by the sensor
  "measuredMaxValue": 1.1,
  // REQUIRED. The unit of the data representing the measured entity
  "measuredUnit": "mm/s", 
  // OPTIONAL. latitude, longitude, altitude in WGS84. Altiude default 0 if only array with lat, long.
  "coordinate": [59.329444, 18.068611, -32.2],
  // OPTIONAL. 
  "battery": "12.76",
  // OPTIONAL. 
  "temperature": "20.0",
  // REQUIRED/OPTIONAL. REQUIRED if time is not specified as a column in data.
  "sampleRate": "1024",
  // REQUIRED/OPTIONAL. REQUIRED if time is not specified as a column in data.
  "numberOfPreTrigSamples": "1024",
  // OPTIONAL. Peak particle velocity, Maximum value of velocity in each axis excluded time column if present
  "peakParticleVelocity": [1.1, 2.2, 3.3],
  // REQUIRED/OPTIONAL. REQUIRED if peakParticleVelocity is defined
  "peakParticleVelocityUnit": "mm/s",
  // OPTIONAL. Peak particle acceleration, Maximum value of acceleration in each axis excluded time column if present
  "peakParticleAcceleration": [1.1, 2.2, 3.3],
  // REQUIRED/OPTIONAL. REQUIRED if peakParticleVelocity is defined
  "peakParticleAccelerationUnit": "m/s2",
  // OPTIONAL. Peak particle displacement, Maximum value of displacement in each axis excluded time column if present
  "peakParticleDisplacement": [1.1, 2.2, 3.3],  
  // REQUIRED/OPTIONAL. REQUIRED if peakParticleVelocity is defined
  "peakParticleDisplacementUnit": "um",
  // OPTIONAL. Zero cutting frequency at the maximum velocity in each axis, unit Hz.
  "zeroCuttingFrequencyVelocity": [1.1, 2.2, 3.3],
  // OPTIONAL. Zero cutting frequency at the maximum acceleration in each axis, unit Hz.
  "zeroCuttingFrequencyAcceleration": [1.1, 2.2, 3.3],
  // OPTIONAL. Sound level, Maximum value of the sound level
  "soundLevelMax": 1.1, 
  // OPTIONAL. Sound level, Average value of the sound level
  "soundLevelAverage": 2.2, 
  // REQUIRED/OPTIONAL. REQUIRED if soundLevelMax is defined. Weighting filter used for the sound level
  "soundLevelWeight": "C" 
  // OPTIONAL. Vibration level, Maximum value of the vibration level
  "vibrationLevelMax": 1.1,
  // OPTIONAL. Vibration level, Minimum value of the top 10% of the vibration level
  "vibrationLevelTop10": 2.2,
  // OPTIONAL. Defalut false, true if has overloaded samples
  "overload":false,
  // REQUIRED enitiy for each column in data
  // If multiple axis use V, L, T for marking vertical longitudinal transverse axis
  "vibrationAxes": ["V", "L", "T"],
  // REQUIRED. unit for each column in data
  "units": ["s", "m/s2","m/s2","m/s2"],
  // REQUIRED.
  "samples": [
    [-1, -1, -1, -1],
    [0, 1.0, 1.0, 1.0],
    [0.00098, 1.1, 1.1, 1.1],
    [10, 10.0, 10.0, 10.0],
  ]
}
```
