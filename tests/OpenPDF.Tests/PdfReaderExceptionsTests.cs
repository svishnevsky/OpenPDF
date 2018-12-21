using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

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
        public void InvalidStreamException()
        {
            using (var stream = new MemoryStream(
                new byte[] { 34, 48, 37, 32, 32 }))
            {
                using (var sut = new PdfReader(stream))
                {
                    sut.ReadVersion();
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ReadVersionDisposedException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfReader(stream);
                sut.Dispose();
                sut.ReadVersion();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ReadObjectException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfReader(stream);
                sut.Dispose();
                sut.ReadObject(null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ReadTrailerException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfReader(stream);
                sut.Dispose();
                sut.ReadTrailer();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ReadCrossReferenceException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfReader(stream);
                sut.Dispose();
                sut.ReadCrossReference();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidReferenceException))]
        public void ReadObjectInvalidReference()
        {
            using (var fileStream =
                new FileStream("example.pdf", FileMode.Open))
            {
                using (var reader = new PdfReader(fileStream))
                {
                    reader.ReadObject(new PdfCrossReference(1, 845, 0, true));
                }
            }
        }
    }
}
