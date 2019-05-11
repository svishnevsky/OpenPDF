using System.Collections.Generic;

namespace OpenPDF.Content
{
    public class CatalogPdfObjectContent : DictionaryPdfObjectContent
    {
        public CatalogPdfObjectContent(
            IDictionary<string, PdfObjectContent> value) : base(value)
        {
        }

        public PdfReference Pages => this.Value<PdfReference>("Pages");
    }
}
