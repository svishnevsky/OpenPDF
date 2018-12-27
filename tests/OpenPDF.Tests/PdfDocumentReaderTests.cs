using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                Assert.AreEqual("1.7", document.Version);
            }
        }
    }
}
