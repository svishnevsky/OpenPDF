using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPDF.Content;

namespace OpenPDF.Tests.Content
{
    [TestClass]
    public class DictionaryPdfObjectContentExtensionsTests
    {
        [TestMethod]
        public void WhenKeyNotExistShouldReturnDefault()
        {
            var content = new DictionaryPdfObjectContent(
                new Dictionary<string, PdfObjectContent>
                {
                    { "key", new PdfObjectContent(null) }
                });

            object value = content.Value<object>("invalidkey");

            Assert.AreEqual(default(object), value);
        }

        [TestMethod]
        public void WhenValueIsIntAndExpectStringShouldChangeType()
        {
            const string Key = "key";
            var content = new DictionaryPdfObjectContent(
                new Dictionary<string, PdfObjectContent>
                {
                    { Key, new PdfObjectContent(12) }
                });

            string value = content.Value<string>(Key);

            Assert.AreEqual("12", value);
        }
    }
}
