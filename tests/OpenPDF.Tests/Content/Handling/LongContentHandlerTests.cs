using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.Content;
using OpenPDF.Content.Handling;

namespace OpenPDF.Tests.Content.Handling
{
    [TestClass]
    public class LongContentHandlerTests
    {
        [DataTestMethod]
        [DataRow("1.54")]
        [DataRow("as")]
        [DataRow("true")]
        public void HandleNonLongContent(string input)
        {
            var sut = new LongContentHandler(null);

            PdfObjectContent result = sut.Handle(input);

            Assert.IsNull(result);
        }

        [DataTestMethod]
        [DataRow("01", 1L)]
        [DataRow("7845", 7845L)]
        public void HandleLongContent(string input, long expectedValue)
        {
            var expected = new LongPdfObjectContent(expectedValue);
            var sut = new LongContentHandler(null);

            PdfObjectContent result = sut.Handle(input);

            Assert.AreEqual(expected, result);
        }
    }
}
