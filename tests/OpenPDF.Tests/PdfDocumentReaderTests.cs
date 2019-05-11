using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.Content;
using OpenPDF.Content.Handling;

namespace OpenPDF.Tests
{
    [TestClass]
    public class PdfDocumentReaderTests
    {
        [TestMethod]
        public void StreamNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new PdfDocumentReader(null));
        }

        [TestMethod]
        public async Task ReadDisposedException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfDocumentReader(stream);
                sut.Dispose();
                await Assert.ThrowsExceptionAsync<ObjectDisposedException>(
                    async () => await sut.ReadDocument());
            }
        }

        [TestMethod]
        public async Task ReadDocument()
        {
            using (var stream = new FileStream("example.pdf", FileMode.Open))
            {
                PdfDocument expected = await GetExpected(stream);
                using (var sut = new PdfDocumentReader(
                    stream))
                {
                    PdfDocument document = await sut.ReadDocument();
                    Assert.AreEqual(expected, document);
                }
            }
        }

        private static async Task<PdfDocument> GetExpected(Stream stream)
        {
            return new PdfDocument(
                "1.7",
                GetExpectedPdfInfo(),
                await GetExpectedPages(stream));
        }

        private static PdfInfo GetExpectedPdfInfo()
        {
            return new PdfInfo(new DictionaryPdfObjectContent(
                new Dictionary<string, PdfObjectContent>
                {
                        { "Author", new StringPdfObjectContent("s.vishnevsky") },
                        { "CreationDate", new DatePdfObjectContent(new DateTime(2018, 12, 4, 10, 53, 21)) },
                        { "ModDate", new DatePdfObjectContent(new DateTime(2018, 12, 4, 10, 53, 21)) },
                        { "Producer", new StringPdfObjectContent("Microsoft: Print To PDF") },
                        { "Title", new StringPdfObjectContent("svishnevsky/OpenPDF: .Net Core PDF reading library") }
                 }));
        }

        private static async Task<PagePdfObjectContent[]> GetExpectedPages(
            Stream stream)
        {
            var reader = new PdfReader(stream);
            var streamHandler = new StreamContentHandler(
                null,
                new DictionaryContentHandler(
                    null,
                    new DefaultContentHandler(),
                    new DictionaryPdfContentFactory()));
            return new[] { new PagePdfObjectContent(
                    new Dictionary<string, PdfObjectContent>
                    {
                        {
                            "Contents",
                            await GetPageContents(reader)
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

        private static async Task<ArrayPdfObjectContent> GetPageContents(
            PdfReader reader)
        {
            return new ArrayPdfObjectContent(
                new PdfObjectContent[]
                {
                    (await reader.ReadObject(new PdfCrossReference(7, 4795, 0, true))).Content,
                    (await reader.ReadObject(new PdfCrossReference(16, 56756, 0, true))).Content,
                    (await reader.ReadObject(new PdfCrossReference(18, 84644, 0, true))).Content,
                });
        }
    }
}
