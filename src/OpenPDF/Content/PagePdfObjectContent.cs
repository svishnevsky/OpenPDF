using System.Collections.Generic;

namespace OpenPDF.Content
{
    public class PagePdfObjectContent : DictionaryPdfObjectContent
    {
        public PagePdfObjectContent(
            IDictionary<string, PdfObjectContent> value) : base(value)
        {
        }
    }
}
