using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

namespace OpenPDF.Tests
{
    [TestClass]
    public class PdfReaderTests
    {
        [TestMethod]
        public async Task ReadVersion()
        {
            const string expected = "1.7";
            using (var stream = new FileStream(
                "example.pdf", FileMode.Open))
            {
                using (var sut = new PdfReader(stream))
                {
                    var result = await sut.ReadVersion();

                    Assert.AreEqual(expected, result);
                }
            }
        }
    }
}