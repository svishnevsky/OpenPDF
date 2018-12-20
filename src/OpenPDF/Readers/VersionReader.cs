using System.IO;

namespace OpenPDF.Readers
{
    internal class VersionReader
    {
        public string Read(FileStreamReader reader)
        {
            reader.SeekToStart();
            string versionLine = reader.ReadLine();
            if (!versionLine.StartsWith(PdfTags.VersionPrefix))
            {
                throw new InvalidDataException(
                    ErrorMessages.InvalidStreamFormat);
            }

            return versionLine.Substring(PdfTags.VersionPrefix.Length);
        }
    }
}
