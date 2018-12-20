using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenPDF.Tests
{
    [TestClass]
    public class PdfReaderTests
    {
        private Stream fileStream;

        [TestInitialize]
        public void Setup()
        {
            this.fileStream = new FileStream("example.pdf", FileMode.Open);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.fileStream.Dispose();
        }

        [TestMethod]
        public void ReadVersion()
        {
            const string Expected = "1.7";
            using (var sut = new PdfReader(this.fileStream))
            {
                string result = sut.ReadVersion();

                Assert.AreEqual(Expected, result);
            }
        }

        [TestMethod]
        public void ReadRawObjects()
        {
            const int Expected = 20;
            using (var sut = new PdfReader(this.fileStream))
            {
                IEnumerable<PdfObject> result = sut.ReadObjects();

                Assert.AreEqual(Expected, result.Count());
            }
        }

        [TestMethod]
        public void ReadTrailer()
        {
            var expected = new PdfTrailer(
                91785,
                21,
                new PdfReference(1, 0),
                new PdfReference(20, 0));
            using (var sut = new PdfReader(this.fileStream))
            {
                PdfTrailer result = sut.ReadTrailer();

                Assert.AreEqual(expected, result);
            }
        }
    }
}
