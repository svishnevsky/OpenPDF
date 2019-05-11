using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.Content;
using OpenPDF.Content.Handling;

namespace OpenPDF.Tests.Content.Handling
{
    [TestClass]
    public class ReferenceContentHandlerTests
    {
        [DataTestMethod]
        [DataRow("1 a R")]
        [DataRow("a 1 R")]
        [DataRow("2 1")]
        [DataRow("2 1 T")]
        [DataRow("some text")]
        public void HandleNonReferenceContent(string input)
        {
            var sut = new ReferenceContentHandler(null);

            PdfObjectContent result = sut.Handle(input);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void HandleReferenceContent()
        {
            var expected = new ReferencePdfObjectContent(
                new PdfReference(2, 1));
            var sut = new ReferenceContentHandler(null);

            PdfObjectContent result = sut.Handle($"2 1 R");

            Assert.AreEqual(expected, result);
        }
    }
}
