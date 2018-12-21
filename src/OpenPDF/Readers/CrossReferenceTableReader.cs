﻿using System.IO;

namespace OpenPDF.Readers
{
    internal class CrossReferenceTableReader
    {
        public PdfCrossReferenceTable Read(
            FileStreamReader reader, long xrefSeek)
        {
            reader.Seek(xrefSeek, SeekOrigin.Begin);
            reader.ReadLine();
            string[] objectsRange = reader.ReadLine().Split(' ');
            int startObject = int.Parse(objectsRange[0]);
            int objectCount = int.Parse(objectsRange[1]);
            var table = new PdfCrossReferenceTable();
            for (int i = startObject; i < startObject + objectCount; i++)
            {
                string reference = reader.ReadLine();
                table.Add(new PdfCrossReference(
                    i,
                    long.Parse(reference.Substring(0, 10)),
                    int.Parse(reference.Substring(11, 5)),
                    reference.Substring(17, 1) == "n"));
            }

            return table;
        }
    }
}
