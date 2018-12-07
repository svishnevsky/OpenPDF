using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task ReadVersion()
        {
            const string expected = "1.7";
            using (var sut = new PdfReader(this.fileStream))
            {
                var result = await sut.ReadVersion();

                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void ReadRawObjects()
        {
            const int expected = 20;
            using (var sut = new PdfReader(this.fileStream))
            {
                var result = sut.ReadObjects();

                Assert.AreEqual(expected, result.Count());
            }
        }
    }
}