using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenPDF.Tests
{
    [TestClass]
    public class FileStreamReaderTests
    {
        [TestMethod]
        public void ReadLineTopToButtom()
        {
            string[] expected = new[] { "%PDF-1.7", "", "4 0 obj" };
            using (var stream = new FileStream(
                "example.pdf", FileMode.Open))
            {
                using (var reader = new FileStreamReader(stream))
                {
                    for (int i = 0; i < expected.Length; i++)
                    {
                        Assert.AreEqual(expected[i], reader.ReadLine());
                    }
                }
            }
        }

        [TestMethod]
        public void ReadLineButtomToTop()
        {
            string[] expected = new[] { "%%EOF", "91785", "startxref" };
            using (var stream = new FileStream(
                "example.pdf", FileMode.Open))
            {
                using (var reader = new FileStreamReader(stream))
                {
                    stream.Seek(0, SeekOrigin.End);
                    for (int i = 0; i < expected.Length; i++)
                    {
                        Assert.AreEqual(
                            expected[i],
                            reader.ReadLine(ReadDirection.BottomToTop));
                    }
                }
            }
        }

        [TestMethod]
        public void ReadLineUpDown()
        {
            string expected = "%%EOF";
            using (var stream = new FileStream(
                "example.pdf", FileMode.Open))
            {
                using (var reader = new FileStreamReader(stream))
                {
                    reader.Seek(0, SeekOrigin.End);
                    Assert.AreEqual(
                            expected,
                            reader.ReadLine(ReadDirection.BottomToTop));
                    Assert.AreEqual(
                            expected,
                            reader.ReadLine());
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullStream()
        {
            var sut = new FileStreamReader(null);
        }
    }
}
