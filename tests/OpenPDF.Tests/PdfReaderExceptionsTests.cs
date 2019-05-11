using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenPDF.Tests
{
    [TestClass]
    public class PdfReaderExceptionsTests
    {
        [TestMethod]
        public void StreamNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new PdfReader(null));
        }

        [TestMethod]
        public async Task InvalidStreamException()
        {
            using (var stream = new MemoryStream(
                new byte[] { 34, 48, 37, 32, 32 }))
            {
                using (var sut = new PdfReader(stream))
                {
                    await Assert.ThrowsExceptionAsync<InvalidDataException>(
                        async () => await sut.ReadVersion());
                }
            }
        }

        [TestMethod]
        public async Task ReadVersionDisposedException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfReader(stream);
                sut.Dispose();
                await Assert.ThrowsExceptionAsync<ObjectDisposedException>(
                    async () => await sut.ReadVersion());
            }
        }

        [TestMethod]
        public async Task ReadObjectException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfReader(stream);
                sut.Dispose();
                await Assert.ThrowsExceptionAsync<ObjectDisposedException>(
                    async () => await sut.ReadObject(null));
            }
        }

        [TestMethod]
        public async Task ReadTrailerException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfReader(stream);
                sut.Dispose();
                await Assert.ThrowsExceptionAsync<ObjectDisposedException>(
                    async () => await sut.ReadTrailer());
            }
        }

        [TestMethod]
        public async Task ReadCrossReferenceException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfReader(stream);
                sut.Dispose();
                await Assert.ThrowsExceptionAsync<ObjectDisposedException>(
                    async () => await sut.ReadCrossReference());
            }
        }

        [TestMethod]
        public async Task ReadObjectInvalidReference()
        {
            using (var fileStream =
                new FileStream("example.pdf", FileMode.Open))
            {
                using (var reader = new PdfReader(fileStream))
                {
                    await Assert.ThrowsExceptionAsync<InvalidReferenceException>(
                        async () => await reader.ReadObject(
                            new PdfCrossReference(1, 845, 0, true)));
                }
            }
        }
    }
}
