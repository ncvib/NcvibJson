namespace NcvibJson.Triggered.V0_1
{
    public interface ITransientNcvibJsonLoader
    {
        TransientNcvibJson LoadFromFile(string fileName);
        TransientNcvibJson LoadFromCompressedFile(string fileName);
    }
}