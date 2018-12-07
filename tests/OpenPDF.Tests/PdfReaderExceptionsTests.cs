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
                new byte[] { 1, 5, 1, 9, 11 }))
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
    }
}