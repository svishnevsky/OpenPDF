using System.IO;
using System.Threading.Tasks;

namespace OpenPDF
{
    public class PdfReader
    {
        private static readonly int versionPrefixLength = "%PDF-".Length;
        private readonly Stream stream;

        public PdfReader(Stream stream)
        {
            this.stream = stream;
        }

        public async Task<string> ReadVersion()
        {
            using (var reader = new StreamReader(this.stream))
            {
                var versionLine = await reader.ReadLineAsync();
                return versionLine.Substring(versionPrefixLength);
            }
        }
    }
}