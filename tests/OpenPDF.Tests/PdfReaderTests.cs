using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.Content;

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
            const string Expected = "1.7";
            using (var sut = new PdfReader(this.fileStream))
            {
                string result = await sut.ReadVersion();

                Assert.AreEqual(Expected, result);
            }
        }

        [TestMethod]
        public async Task ReadObject()
        {
            var expected = new PdfObject(1, 0, new DictionaryPdfObjectContent(
                new Dictionary<string, PdfObjectContent>
                {
                    {
                        "Pages",
                        new ReferencePdfObjectContent(
                            new PdfReference(2, 0))
                    },
                    {
                        "Type",
                        new TypePdfObjectContent("Catalog")
                    }
                }));
            var reference = new PdfCrossReference(1, 91519, 0, true);
            using (var sut = new PdfReader(this.fileStream))
            {
                PdfObject result = await sut.ReadObject(reference);
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public async Task ReadTrailer()
        {
            var expected = new PdfTrailer(
                91785,
                21,
                new PdfReference(1, 0),
                new PdfReference(20, 0));
            using (var sut = new PdfReader(this.fileStream))
            {
                PdfTrailer result = await sut.ReadTrailer();

                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public async Task ReadCrossReference()
        {
            PdfCrossReferenceTable expected = GetReferencesTable();
            using (var sut = new PdfReader(this.fileStream))
            {
                PdfCrossReferenceTable result = 
                    await sut.ReadCrossReference();

                Assert.AreEqual(expected, result);
            }
        }

        private static PdfCrossReferenceTable GetReferencesTable()
        {
            var expected = new PdfCrossReferenceTable();
            expected.Add(new PdfCrossReference(0, 0, 65535, false));
            expected.Add(new PdfCrossReference(1, 91519, 0, true));
            expected.Add(new PdfCrossReference(2, 91460, 0, true));
            expected.Add(new PdfCrossReference(3, 91284, 0, true));
            expected.Add(new PdfCrossReference(4, 9, 0, true));
            expected.Add(new PdfCrossReference(5, 1572, 0, true));
            expected.Add(new PdfCrossReference(6, 3190, 0, true));
            expected.Add(new PdfCrossReference(7, 4795, 0, true));
            expected.Add(new PdfCrossReference(8, 31554, 0, true));
            expected.Add(new PdfCrossReference(9, 33833, 0, true));
            expected.Add(new PdfCrossReference(10, 37864, 0, true));
            expected.Add(new PdfCrossReference(11, 40518, 0, true));
            expected.Add(new PdfCrossReference(12, 44592, 0, true));
            expected.Add(new PdfCrossReference(13, 47203, 0, true));
            expected.Add(new PdfCrossReference(14, 49703, 0, true));
            expected.Add(new PdfCrossReference(15, 52223, 0, true));
            expected.Add(new PdfCrossReference(16, 56756, 0, true));
            expected.Add(new PdfCrossReference(17, 83105, 0, true));
            expected.Add(new PdfCrossReference(18, 84644, 0, true));
            expected.Add(new PdfCrossReference(19, 91069, 0, true));
            expected.Add(new PdfCrossReference(20, 91568, 0, true));
            return expected;
        }
    }
}
