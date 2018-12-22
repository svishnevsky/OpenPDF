using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.ContentHandling;

namespace OpenPDF.Tests.ContentHandling
{
    [TestClass]
    public class BoolContentHandlerTests
    {
        [DataTestMethod]
        [DataRow("value")]
        public void NotBoolValue(string input)
        {
            var sut = new BoolContentHandler(null);

            PdfObjectContent result = sut.Handle(input);

            Assert.IsNull(result);
        }

        [DataTestMethod]
        [DataRow("true", true)]
        [DataRow("false", false)]
        public void BoolValue(string input, bool expectedValue)
        {
            var expected = new BoolPdfObjectContent(expectedValue);
            var sut = new BoolContentHandler(null);

            PdfObjectContent result = sut.Handle(input);

            Assert.AreEqual(expected, result);
        }
    }
}
