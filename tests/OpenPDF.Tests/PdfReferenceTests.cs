using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenPDF.Tests
{
    [TestClass]
    public class PdfReferenceTests
    {
        [TestMethod]
        public void Parse()
        {
            var expected = new PdfReference(3, 7);
            var result = PdfReference.Parse("3 7 R");

            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("3 7 obj")]
        [DataRow("3 7")]
        [DataRow("3 7 R 0")]
        [DataRow("a 7 R")]
        [DataRow("3 z R")]
        [ExpectedException(typeof(FormatException))]
        public void ParseInvalidFormat(string input)
        {
            var result = PdfReference.Parse(input);
        }
    }
}
