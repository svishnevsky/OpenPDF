using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OpenPDF.Tests
{
    [TestClass]
    public class PdfReaderExceptionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StreamNullException()
        {
            new PdfReader(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public async Task InvalidStreamException()
        {
            using (var stream = new MemoryStream(
                new byte[] { 34, 48, 37, 32, 32 }))
            {
                using (var sut = new PdfReader(stream))
                {
                    await sut.ReadVersion();
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task ReadVersionDisposedException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfReader(stream);
                sut.Dispose();
                await sut.ReadVersion();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task ReadObjectException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfReader(stream);
                sut.Dispose();
                await sut.ReadObject(null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task ReadTrailerException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfReader(stream);
                sut.Dispose();
                await sut.ReadTrailer();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task ReadCrossReferenceException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfReader(stream);
                sut.Dispose();
                await sut.ReadCrossReference();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidReferenceException))]
        public async Task ReadObjectInvalidReference()
        {
            using (var fileStream =
                new FileStream("example.pdf", FileMode.Open))
            {
                using (var reader = new PdfReader(fileStream))
                {
                    await reader.ReadObject(
                        new PdfCrossReference(1, 845, 0, true));
                }
            }
        }
    }
}
