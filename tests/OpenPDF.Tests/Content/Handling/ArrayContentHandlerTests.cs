using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.Content;
using OpenPDF.Content.Handling;

namespace OpenPDF.Tests.Content.Handling
{
    [TestClass]
    public class ArrayContentHandlerTests
    {
        [DataTestMethod]
        [DataRow("[1 true")]
        [DataRow("1 [true]]")]
        [DataRow("(string)")]
        public void HandleNonArrayContent(string content)
        {
            var sut = new ArrayContentHandler(null, null);

            PdfObjectContent result = sut.Handle(content);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void HandleArrayContent()
        {
            var expected = new ArrayPdfObjectContent(
                new PdfObjectContent[]
                {
                    new BoolPdfObjectContent(true),
                    new TypePdfObjectContent("Type")
                });
            var sut = new ArrayContentHandler(
                null,
                new DefaultContentHandler());

            PdfObjectContent result = sut.Handle("[true /Type]");

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void HandleComplexArrayContent()
        {
            var expected = new ArrayPdfObjectContent(
                new PdfObjectContent[]
                {
                    new StringPdfObjectContent("text"),
                    new ArrayPdfObjectContent(new PdfObjectContent[]
                    {
                        new BoolPdfObjectContent(true),
                        new TypePdfObjectContent("Type")
                    }),
                    new ArrayPdfObjectContent(new PdfObjectContent[0])
                });
            var sut = new ArrayContentHandler(
                null,
                new DefaultContentHandler());

            PdfObjectContent result = sut.Handle("[(text) [true /Type] []]");

            Assert.AreEqual(expected, result);
        }
    }
}
