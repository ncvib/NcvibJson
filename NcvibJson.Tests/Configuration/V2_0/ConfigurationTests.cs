using System.Text.Json;
using NcvibJson.Common.Definitions.V2_0;

namespace NcvibJson.Tests.Configuration.V2_0;

[TestFixture]
public class ConfigurationTests
{
    protected SchemaValidator Validator;
    protected readonly JsonSerializerOptions JsonSerializerOptions = new() {WriteIndented = true };
    
    [SetUp]
    public void Setup()
    {
        Validator = new SchemaValidator();
    }
    
    [Test]
    public void ConfigurationShouldDefaultToVersion2_0()
    {
        var configuration = CreateBasicConfiguration();
        Assert.That(configuration.FormatVersion, Is.EqualTo("2.0"));        
    }
    
    [Test]
    public void ConfigurationShouldDefaultToTimeZoneEtcUtc()
    {
        var configuration = CreateBasicConfiguration();
        Assert.That(configuration.InstrumentIanaTimezone, Is.EqualTo("Etc/UTC"));        
    }    
    
    [Test]
    public void ConfigurationShouldDefaultToAllHoursActive()
    {
        var configuration = CreateBasicConfiguration();
        var index = 0;
        configuration.ActiveHours.ForEach(h =>
        {
            Assert.That(h, Is.EqualTo(index++));    
        });
        
        Assert.That(configuration.ActiveHours.Count, Is.EqualTo(24));
    }        
    
    protected NcvibJson.Configuration.V2_0.Configuration CreateBasicConfiguration()
    {
        return new NcvibJson.Configuration.V2_0.Configuration
        {
            FormatVersion = "2.0",
            InstrumentDefinition = new InstrumentDefinition
            {
                FormatVersion = "2.0",
                Company = "AnyCompany",
                SensorType = "AnyInstrumentType",
                SensorSerialNumber = 123456
            }
        };
    }
}