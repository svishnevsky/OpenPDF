using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.Content;
using OpenPDF.Content.Handling;

namespace OpenPDF.Tests.Content.Handling
{
    [TestClass]
    public class NullContentHandlerTests
    {
        [DataTestMethod]
        [DataRow("value")]
        [DataRow("true")]
        [DataRow("")]
        public void HandleNonNullContent(string content)
        {
            var sut = new NullContentHandler(null);

            PdfObjectContent result = sut.Handle(content);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void HandleNullContent()
        {
            var expected = new NullPdfObjectContent();
            var sut = new NullContentHandler(null);

            PdfObjectContent result = sut.Handle("null");

            Assert.AreEqual(expected, result);
        }
    }
}
