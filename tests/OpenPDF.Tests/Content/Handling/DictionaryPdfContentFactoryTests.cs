using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.Content;
using OpenPDF.Content.Handling;

namespace OpenPDF.Tests.Content.Handling
{
    [TestClass]
    public class DictionaryPdfContentFactoryTests
    {
        [TestMethod]
        public void CreateDefaultDictionary()
        {
            var props = new Dictionary<string, PdfObjectContent>
            {
                { "Some Prop", new NullPdfObjectContent() }
            };
            var expected = new DictionaryPdfObjectContent(props);
            var sut = new DictionaryPdfContentFactory();

            DictionaryPdfObjectContent result = sut.Create(props);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CreateUnlnownTypeDictionary()
        {
            var props = new Dictionary<string, PdfObjectContent>
            {
                { "Type", new StringPdfObjectContent("someunknowntype") }
            };
            var expected = new DictionaryPdfObjectContent(props);
            var sut = new DictionaryPdfContentFactory();

            DictionaryPdfObjectContent result = sut.Create(props);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CreateCatalogDictionary()
        {
            var props = new Dictionary<string, PdfObjectContent>
            {
                { "Type", new StringPdfObjectContent("Catalog") }
            };
            var expected = new CatalogPdfObjectContent(props);
            var sut = new DictionaryPdfContentFactory();

            DictionaryPdfObjectContent result = sut.Create(props);

            Assert.IsInstanceOfType(result, typeof(CatalogPdfObjectContent));
            Assert.AreEqual(expected, result);
        }
    }
}
