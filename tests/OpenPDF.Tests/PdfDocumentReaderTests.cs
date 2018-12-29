using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.Content;

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
                GetExpectedPdfInfo(),
                GetExpectedPages());
        }

        private static PdfInfo GetExpectedPdfInfo()
        {
            return new PdfInfo(new DictionaryPdfObjectContent(
                new Dictionary<string, PdfObjectContent>
                {
                        { "Author", new StringPdfObjectContent("s.vishnevsky") },
                        { "CreationDate", new DatePdfObjectContent(new DateTime(2018, 12, 4, 13, 53, 21)) },
                        { "ModDate", new DatePdfObjectContent(new DateTime(2018, 12, 4, 13, 53, 21)) },
                        { "Producer", new StringPdfObjectContent("Microsoft: Print To PDF") },
                        { "Title", new StringPdfObjectContent("svishnevsky/OpenPDF: .Net Core PDF reading library") }
                 }));
        }

        private static PagePdfObjectContent[] GetExpectedPages()
        {
            return new[] { new PagePdfObjectContent(
                    new Dictionary<string, PdfObjectContent>
                    {
                        {
                            "Contents",
                            new ArrayPdfObjectContent(
                                new PdfObjectContent[]
                                {
                                    new ReferencePdfObjectContent(new PdfReference(7, 0)),
                                    new ReferencePdfObjectContent(new PdfReference(16, 0)),
                                    new ReferencePdfObjectContent(new PdfReference(18, 0))
                                })
                        },
                        {
                            "CropBox",
                            new ArrayPdfObjectContent(
                                new PdfObjectContent[]
                                {
                                    new FloatPdfObjectContent(0.0f),
                                    new FloatPdfObjectContent(0.0f),
                                    new FloatPdfObjectContent(612.0f),
                                    new FloatPdfObjectContent(792.0f)
                                })
                        },
                        {
                            "MediaBox",
                            new ArrayPdfObjectContent(
                                new PdfObjectContent[]
                                {
                                    new FloatPdfObjectContent(0.0f),
                                    new FloatPdfObjectContent(0.0f),
                                    new FloatPdfObjectContent(612.0f),
                                    new FloatPdfObjectContent(792.0f)
                                })
                        },
                        {
                            "Parent",
                            new ReferencePdfObjectContent(new PdfReference(2, 0))
                        },
                        {
                            "Resources",
                            new ReferencePdfObjectContent(new PdfReference(19, 0))
                        },
                        { "Rotate", new LongPdfObjectContent(0) },
                        { "Type", new TypePdfObjectContent("Page") }
                    })
                };
        }
    }
}
