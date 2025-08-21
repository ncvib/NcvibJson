using System.Text.Json;
using System.Text.Json.Serialization;

namespace NcvibJson.Tests.Triggered;

[TestFixture]
public class WhenATransientDataJsonFileIsDeserialized
{
    [Test]
    public void Should_Deserialize_Correctly()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        options.Converters.Add(new JsonStringEnumConverter());

        var transientData = JsonSerializer.Deserialize<NcvibJson.Triggered.V2_0.TriggeredData>(Json, options);
        Assert.That(transientData, Is.Not.Null);
    }

    private const string Json = """
                                {
                                  "formatVersion": "2.0",
                                  "instrument": {
                                    "formatVersion": "2.0",
                                    "company": "NorSonic",
                                    "sensorType": "Nor1702",
                                    "sensorSerialNumber": 163000012
                                  },
                                  "port": 1,
                                  "coordinates": null,
                                  "battery": null,
                                  "temperature": null,
                                  "overload": false,
                                  "filteredSamples": false,
                                  "standard": {
                                    "name": "Unknown",
                                    "filterDefinition": {
                                      "lowPass": 0,
                                      "highPass": 1000
                                    }
                                  },
                                  "measuredQuantity": "Voltage",
                                  "measuredUnit": "V",
                                  "measuredMaxValue": [
                                    6.222009181976318,
                                    -2.861905097961426,
                                    -5.164370059967041
                                  ],
                                  "peakParticleVelocity": [],
                                  "peakParticleVelocityUnit": "mm/s",
                                  "peakParticleAcceleration": [],
                                  "peakParticleAccelerationUnit": "mm/s^2",
                                  "peakParticleDisplacement": [],
                                  "peakParticleDisplacementUnit": "mm",
                                  "zeroCuttingFrequencyVelocity": [],
                                  "zeroCuttingFrequencyVelocityUnit": "Hz",
                                  "zeroCuttingFrequencyAcceleration": [],
                                  "zeroCuttingFrequencyAccelerationUnit": "Hz",
                                  "startTime": "2025-08-20T14:13:45Z",
                                  "sampleRate": 5120,
                                  "numberOfPreTriggerSamples": 0,
                                  "axes": [
                                    "Vertical",
                                    "Longitudinal",
                                    "Transversal"
                                  ],
                                  "transformationMatrix": [],
                                  "triggerLevel": [
                                    0,
                                    0,
                                    0
                                  ],
                                  "triggerLevelUnit": "V",
                                  "maxSamples": [
                                    {
                                      "axis": "Vertical",
                                      "quantity": "Voltage",
                                      "unit": "V",
                                      "value": [
                                        6.222009181976318,
                                        -2.435145854949951,
                                        -3.4236366748809814
                                      ],
                                      "timeOffsetInSeconds": 10.498828125
                                    },
                                    {
                                      "axis": "Longitudinal",
                                      "quantity": "Voltage",
                                      "unit": "V",
                                      "value": [
                                        4.561151504516602,
                                        -2.861905097961426,
                                        -1.7021836042404175
                                      ],
                                      "timeOffsetInSeconds": 10.49375
                                    },
                                    {
                                      "axis": "Transversal",
                                      "quantity": "Voltage",
                                      "unit": "V",
                                      "value": [
                                        0.45169854164123535,
                                        -1.168619155883789,
                                        -5.164370059967041
                                      ],
                                      "timeOffsetInSeconds": 10.5525390625
                                    }
                                  ],
                                  "sampleUnits": [
                                    "s",
                                    "V",
                                    "V",
                                    "V"
                                  ],
                                  "samples": [
                                    [
                                      0,
                                      0.010974263772368431,
                                      -0.0001183499043690972,
                                      0.005497890990227461
                                    ],
                                    [
                                      0.0001953125,
                                      0.012254593893885612,
                                      -0.002291684504598379,
                                      0.01100654061883688
                                    ]
                                  ]
                                }
                                """;
}