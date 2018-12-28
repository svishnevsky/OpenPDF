using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.ContentHandling;

namespace OpenPDF.Tests
{
    [TestClass]
    public class PdfDocumentReaderTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StreamNullException()
        {
            new PdfDocumentReader(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task ReadDisposedException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfDocumentReader(stream);
                sut.Dispose();
                await sut.ReadDocument();
            }
        }

        [TestMethod]
        public async Task ReadDocument()
        {
            using (var sut = new PdfDocumentReader(
                new FileStream("example.pdf", FileMode.Open)))
            {
                PdfDocument document = await sut.ReadDocument();
                Assert.AreEqual(GetExpected(), document);
            }
        }

        private static PdfDocument GetExpected()
        {
            return new PdfDocument(
                "1.7",
                new PdfInfo(new DictionaryPdfObjectContent(
                    new Dictionary<string, PdfObjectContent>
                    {
                        { "Author", new StringPdfObjectContent("s.vishnevsky") },
                        { "CreationDate", new DatePdfObjectContent(new DateTime(2018, 12, 4, 13, 53, 21)) },
                        { "ModDate", new DatePdfObjectContent(new DateTime(2018, 12, 4, 13, 53, 21)) },
                        { "Producer", new StringPdfObjectContent("Microsoft: Print To PDF") },
                        { "Title", new StringPdfObjectContent("svishnevsky/OpenPDF: .Net Core PDF reading library") }
                    })));
        }
    }
}
