using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.ContentHandling;

namespace OpenPDF.Tests.ContentHandling
{
    [TestClass]
    public class FloatContentHandlerTests
    {
        [DataTestMethod]
        [DataRow("as")]
        [DataRow("true")]
        public void HandleNonFloatContent(string input)
        {
            var sut = new FloatContentHandler(null);

            PdfObjectContent result = sut.Handle(input);

            Assert.IsNull(result);
        }

        [DataTestMethod]
        [DataRow("0.1", 0.1f)]
        [DataRow("54.125", 54.125f)]
        public void HandleFloatContent(string input, float expectedValue)
        {
            var expected = new FloatPdfObjectContent(expectedValue);
            var sut = new FloatContentHandler(null);

            PdfObjectContent result = sut.Handle(input);

            Assert.AreEqual(expected, result);
        }
    }
}
