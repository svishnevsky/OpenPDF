using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.Content;
using OpenPDF.Content.Handling;

namespace OpenPDF.Tests.Content.Handling
{
    [TestClass]
    public class DateContentHandlerTests
    {
        [DataTestMethod]
        [DataRow("1")]
        [DataRow("(some text)")]
        [DataRow("(Dsome text)")]
        [DataRow("(D:some text")]
        public void HandleNonDateContent(string input)
        {
            var sut = new DateContentHandler(null);

            PdfObjectContent result = sut.Handle(input);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void HandleDateContent()
        {
            var expected = new DatePdfObjectContent(
                new DateTime(2018, 12, 04, 10, 53, 21, DateTimeKind.Utc));
            var sut = new DateContentHandler(null);

            PdfObjectContent result = sut.Handle(
                "(D:20181204135321+03'00')");

            Assert.AreEqual(expected, result);
        }
    }
}
