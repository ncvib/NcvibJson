using System.IO.Compression;
using Newtonsoft.Json;

namespace NcvibJson.Triggered.V0_1
{
    public class TransientNcvibJsonLoader : ITransientNcvibJsonLoader
    {
        public TransientNcvibJson LoadFromFile(string fileName)
        {
            var item = Deserialize<TransientNcvibJson>(File.ReadAllText(fileName));
            return item;
        }

        public TransientNcvibJson LoadFromCompressedFile(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open))
            using (var gz = new GZipStream(fs, CompressionMode.Decompress))
            using (var sr = new StreamReader(gz))
            {
                var code = sr.ReadToEnd();
                return Deserialize<TransientNcvibJson>(code);
            }
        }
        
        private T Deserialize<T>(string output)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };

            return JsonConvert.DeserializeObject<T>(output, settings);
        }
    }
}