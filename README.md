# Specification for Ncvib json

Version 0.1

Example
```json
{
  "sensorType": "sensorA",
  "serialNumber": "1234" ,
  "time": "2018-01-16T15:26:00+1:00",
  "coordinates": [59.329444, 18.068611, -32.2],
  "battery": "12.76",
  "temperature": "20.0",
  "sampleRate": "1024",
  "entity": ["Time", "Acceleration-V", "Acceleration-L", "Acceleration-T"],
  "unit": ["s", "m/s2","m/s2","m/s2"],
	"numberOfPreTrigSamples": "1024",
  "peakParticleVelocity": [1.1, 2.2, 3.3],
  "peakParticleVelocityUnit": "mm/s",
  "peakParticleAcceleration": [1.1, 2.2, 3.3],
  "peakParticleAccelerationUnit": "m/s2",
  "peakParticleDisplacement": [1.1, 2.2, 3.3],
  "peakParticleDisplacementUnit": "um",
  "zeroCuttingFrequencyVelocity": [1.1, 2.2, 3.3],
  "zeroCuttingFrequencyAcceleration": [1.1, 2.2, 3.3],
  "overload":false,
  "data": [
    [0, 1.0, 1.0, 1.0],
    [0.00098, 1.1, 1.1, 1.1],
    [10, 10.0, 10.0, 10.0],
  ]
}
```

- Properties
sensorType
Req
String for sensor type
serialNumber
Req
Serial number of the sensor
time
Req
ISO8601 date/time included time zone.
n


North (latitude) coordinate of sensor in WGS 84
e


East (longitude) coordinate of sensor in WGS 84
alt


Altitude of sensor
battery


Battery voltage
temperature


Temperature
sampleRate


Sample rate
dataEntity


The entity of the data: Acceleration, Velocity, ...
dataUnit


The unit of the data: m/s2, mm/s, ...
axis


The order of the axis of the data array:
V - Vertical
L - Longitudinal (Radial)
T - Transversal
numberOfPreTrigSamples


Number of Pre Triggered Samples
ppv


Array. Peak particle velocity, Maximum value of velocity in each axis
ppa


Array. Peak particle acceleration, Maximum value of acceleration in each axis
ppd


Array. Peak particle displacement, Maximum value of displacement in each axis
zeroCutingFreqVelocity


Array. Zero cutting frequency at the maximum velocity in each axis
zeroCutingFreqAcc


Array. Zero cutting frequency at the maximum acceleration in each axis
overload
Req
Overload flag (exists if transient has overloaded sample)
0 - No overload
1 - Has overloaded samples
data
Req
Array of each sample with an array in each axis.





