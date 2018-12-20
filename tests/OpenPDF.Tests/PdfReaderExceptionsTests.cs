using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

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
        public void ReadObjectsException()
        {
            using (var stream = new MemoryStream())
            {
                var sut = new PdfReader(stream);
                sut.Dispose();
                sut.ReadObjects().Any();
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
    }
}
