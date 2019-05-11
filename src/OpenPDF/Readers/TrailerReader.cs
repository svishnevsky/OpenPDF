using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenPDF.Readers
{
    internal class TrailerReader
    {
        public async Task<PdfTrailer> Read(FileStreamReader reader)
        {
            string currentLine = null;
            reader.SeekToEnd();
            while (currentLine != PdfTags.Eof)
            {
                currentLine = await reader.ReadLine(
                    ReadDirection.BottomToTop);
            }

            currentLine = await reader.ReadLine(ReadDirection.BottomToTop);
            long xrefSeek = long.Parse(currentLine);
            Dictionary<string, string> properties =
                await ReadProperties(reader);

            return new PdfTrailer(
                xrefSeek,
                int.Parse(properties["SIZE"]),
                PdfReference.Parse(properties["ROOT"]),
                PdfReference.Parse(properties["INFO"]));
        }

        private static async Task<Dictionary<string, string>> ReadProperties(
            FileStreamReader reader)
        {
            string currentLine = null;
            while (currentLine != PdfTags.DictionaryEnd)
            {
                currentLine = await reader.ReadLine(
                    ReadDirection.BottomToTop);
            }

            currentLine = await reader.ReadLine(
                    ReadDirection.BottomToTop);
            var properties = new Dictionary<string, string>();
            while (currentLine != PdfTags.DictionaryStart)
            {
                var propBuilder = new StringBuilder(currentLine);
                propBuilder.Remove(0, 1);
                string rawProp = propBuilder.ToString();
                int spaceIndex = rawProp.IndexOf(' ');
                properties.Add(
                    rawProp.Substring(0, spaceIndex).ToUpper(),
                    rawProp.Substring(spaceIndex + 1));
                currentLine = await reader.ReadLine(
                    ReadDirection.BottomToTop);
            }

            return properties;
        }
    }
}
