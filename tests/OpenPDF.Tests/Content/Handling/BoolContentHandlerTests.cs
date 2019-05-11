using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.Content;
using OpenPDF.Content.Handling;

namespace OpenPDF.Tests.Content.Handling
{
    [TestClass]
    public class BoolContentHandlerTests
    {
        [DataTestMethod]
        [DataRow("value")]
        public void HandleNonBoolContent(string input)
        {
            var sut = new BoolContentHandler(null);

            PdfObjectContent result = sut.Handle(input);

            Assert.IsNull(result);
        }

        [DataTestMethod]
        [DataRow("true", true)]
        [DataRow("false", false)]
        public void HandleBoolContent(string input, bool expectedValue)
        {
            var expected = new BoolPdfObjectContent(expectedValue);
            var sut = new BoolContentHandler(null);

            PdfObjectContent result = sut.Handle(input);

            Assert.AreEqual(expected, result);
        }
    }
}
