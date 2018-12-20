using System.Collections.Generic;
using System.IO;

namespace OpenPDF.Readers
{
    internal class ObjectReader
    {
        public IEnumerable<PdfObject> Read(FileStreamReader reader)
        {
            reader.SeekToStart();
            string currentLine;
            while ((currentLine = FindNextObjectDefinition(reader))
                    != null)
            {
                string[] objectDefinition = currentLine.Split(' ');
                var objectContent = new List<string>();
                while ((currentLine = reader.ReadLine())
                    != PdfTags.EndObj)
                {
                    objectContent.Add(currentLine);
                }

                yield return new PdfObject(
                    int.Parse(objectDefinition[0]),
                    int.Parse(objectDefinition[1]),
                    objectContent);
            }

            yield break;
        }

        private static string FindNextObjectDefinition(FileStreamReader reader)
        {
            long previousPosition = reader.Position;
            string currentLine = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                if (IsObjectDefinition(currentLine))
                {
                    return currentLine;
                }

                if (PdfTags.Xref.Equals(currentLine) ||
                    PdfTags.Trailer.Equals(currentLine))
                {
                    reader.Seek(previousPosition, SeekOrigin.Begin);
                    return null;
                }

                previousPosition = reader.Position;
                currentLine = reader.ReadLine();
            }

            return null;
        }

        private static bool IsObjectDefinition(string currentLine)
        {
            return !currentLine.StartsWith("%") &&
                currentLine.EndsWith(PdfTags.Obj);
        }
    }
}
