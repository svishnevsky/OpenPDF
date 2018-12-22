using System.Collections.Generic;
using System.Text;

namespace OpenPDF.Readers
{
    internal class TrailerReader
    {
        public PdfTrailer Read(FileStreamReader reader)
        {
            string currentLine = null;
            reader.SeekToEnd();
            while (currentLine != PdfTags.Eof)
            {
                currentLine = reader.ReadLine(
                    ReadDirection.BottomToTop);
            }

            currentLine = reader.ReadLine(ReadDirection.BottomToTop);
            long xrefSeek = long.Parse(currentLine);
            Dictionary<string, string> properties =
                ReadProperties(reader);

            return new PdfTrailer(
                xrefSeek,
                int.Parse(properties["SIZE"]),
                PdfReference.Parse(properties["ROOT"]),
                PdfReference.Parse(properties["INFO"]));
        }

        private static Dictionary<string, string> ReadProperties(
            FileStreamReader reader)
        {
            string currentLine = null;
            while (currentLine != PdfTags.DictionaryEnd)
            {
                currentLine = reader.ReadLine(
                    ReadDirection.BottomToTop);
            }

            currentLine = reader.ReadLine(
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
                currentLine = reader.ReadLine(
                    ReadDirection.BottomToTop);
            }

            return properties;
        }
    }
}
