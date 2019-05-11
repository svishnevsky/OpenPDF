using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenPDF.Tests
{
    [TestClass]
    public class FileStreamReaderTests
    {
        [TestMethod]
        public async Task ReadLineTopToBottom()
        {
            string[] expected = new[] { "%PDF-1.7", "", "4 0 obj" };
            using (var stream = new FileStream(
                "example.pdf", FileMode.Open))
            {
                using (var reader = new FileStreamReader(stream))
                {
                    for (int i = 0; i < expected.Length; i++)
                    {
                        string line = await reader.ReadLine();
                        Assert.AreEqual(expected[i], line);
                    }
                }
            }
        }

        [TestMethod]
        public async Task ReadLineButtomToTop()
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
                        string line = await reader.ReadLine(
                            ReadDirection.BottomToTop);
                        Assert.AreEqual(
                            expected[i],
                            line);
                    }
                }
            }
        }

        [TestMethod]
        public async Task ReadLineUpDown()
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
                            await reader.ReadLine(ReadDirection.BottomToTop));
                    Assert.AreEqual(
                            expected,
                            await reader.ReadLine());
                }
            }
        }

        [TestMethod]
        public void NullStream()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => new FileStreamReader(null));
        }
    }
}
