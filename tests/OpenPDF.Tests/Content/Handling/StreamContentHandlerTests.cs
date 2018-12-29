using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.Content;
using OpenPDF.Content.Handling;

namespace OpenPDF.Tests.Content.Handling
{
    [TestClass]
    public class StreamContentHandlerTests
    {
        [DataTestMethod]
        [DataRow("some string")]
        [DataRow("<<some string")]
        [DataRow("<some string>>stream")]
        [DataRow("<some string>>   endstream")]
        public void HandleNonStream(string input)
        {
            var sut = new StreamContentHandler(null);

            PdfObjectContent result = sut.Handle(input);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void HandleStream()
        {
            var expected = new StreamPdfObjectContent(
                new PdfStream(
                    new Dictionary<string, PdfObjectContent>
                    {
                        { "Length", new LongPdfObjectContent(4) }
                    },
                    "����"));
            var sut = new StreamContentHandler(null);

            PdfObjectContent result = sut.Handle(
                PdfContent.Stream);

            Assert.AreEqual(expected, result);
        }
    }
}
