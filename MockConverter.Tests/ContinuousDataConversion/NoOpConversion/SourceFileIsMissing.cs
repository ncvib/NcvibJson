namespace MockConverter.Tests.ContinuousDataConversion.NoOpConversion;

[TestFixture]
public class SourceFileIsMissing : BaseContext
{
    [SetUp]
    public void Setup()
    {
        GivenAConverter(new NoOpConverter());
        GivenAMissingFile();        
        GivenAContinuousDataFilter(InputFilePath);
    }

    [Test]
    public void ConversionFails()
    {
        Assert.Throws<FileNotFoundException>(() => Converter.Execute(Filter));
    }

    private void GivenAMissingFile()
    {
        InputFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
    }
}