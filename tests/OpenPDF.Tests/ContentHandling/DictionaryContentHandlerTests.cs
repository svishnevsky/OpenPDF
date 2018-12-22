using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.ContentHandling;

namespace OpenPDF.Tests.ContentHandling
{
    [TestClass]
    public class DictionaryContentHandlerTests
    {
        [DataTestMethod]
        [DataRow("some string")]
        [DataRow("1")]
        [DataRow("true")]
        [DataRow("<<some string")]
        [DataRow("<some string>>")]
        public void HandleNonDictioinary(string input)
        {
            var sut = new DictionaryContentHandler(
                null, new DefaultContentHandler());

            PdfObjectContent result = sut.Handle(input);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void HandleDictionary()
        {
            var expected = new DictionaryPdfObjectContent(
                new Dictionary<string, PdfObjectContent>
                {
                    {
                        "Pages",
                        new ReferencePdfObjectContent(
                            new PdfReference(2, 0))
                    },
                    {
                        "Type",
                        new TypePdfObjectContent("Catalog")
                    }
                });
            var sut = new DictionaryContentHandler(
                null, 
                new DefaultContentHandler());

            PdfObjectContent result = sut.Handle(
                PdfContent.CatalogObject);

            Assert.AreEqual(expected, result);
        }
    }
}
