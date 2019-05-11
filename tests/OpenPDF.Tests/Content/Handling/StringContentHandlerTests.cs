using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.Content;
using OpenPDF.Content.Handling;

namespace OpenPDF.Tests.Content.Handling
{
    [TestClass]
    public class StringContentHandlerTests
    {
        [DataTestMethod]
        [DataRow("true")]
        [DataRow("1")]
        [DataRow("(some text")]
        [DataRow("some text)")]
        public void HandleNonStringContent(string input)
        {
            var sut = new StringContentHandler(null);

            PdfObjectContent result = sut.Handle(input);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void HandleStringContent()
        {
            const string Value = "some string";
            var expected = new StringPdfObjectContent(Value);
            var sut = new StringContentHandler(null);

            PdfObjectContent result = sut.Handle($"({Value})");

            Assert.AreEqual(expected, result);
        }
    }
}
