using System.IO;
using System.Threading.Tasks;

namespace OpenPDF.Readers
{
    internal class VersionReader
    {
        public async Task<string> Read(FileStreamReader reader)
        {
            reader.SeekToStart();
            string versionLine = await reader.ReadLine();
            if (!versionLine.StartsWith(PdfTags.VersionPrefix))
            {
                throw new InvalidDataException(
                    ErrorMessages.InvalidStreamFormat);
            }

            return versionLine.Substring(PdfTags.VersionPrefix.Length);
        }
    }
}
