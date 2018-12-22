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
            DictionaryPdfObjectContent expected = GetExpectedDictionary();
            var sut = new DictionaryContentHandler(
                null,
                new DefaultContentHandler());

            PdfObjectContent result = sut.Handle(
                PdfContent.Dictionary);

            Assert.AreEqual(expected, result);
        }

        private static DictionaryPdfObjectContent GetExpectedDictionary()
        {
            return new DictionaryPdfObjectContent(
                new Dictionary<string, PdfObjectContent>
                {
                    {
                        "Pages",
                        new ReferencePdfObjectContent(
                            new PdfReference(2, 0))
                    },
                    {
                        "Type", new TypePdfObjectContent("Catalog")
                    },
                    {
                        "Text", new StringPdfObjectContent("some text")
                    },
                    {
                        "Dict", new DictionaryPdfObjectContent(
                            new Dictionary<string, PdfObjectContent>
                            {
                                {
                                    "Type",
                                    new TypePdfObjectContent("DictType")
                                },
                                {
                                    "Dict", new DictionaryPdfObjectContent(
                                        new Dictionary<string, PdfObjectContent>
                                        {
                                            {
                                                "Dict", new DictionaryPdfObjectContent(
                                                    new Dictionary<string, PdfObjectContent>())
                                            }
                                        })
                                },
                                {
                                    "Dict1", new DictionaryPdfObjectContent(
                                        new Dictionary<string, PdfObjectContent>())
                                }
                            })
                    }
                });
        }
    }
}
