using System.Collections.Generic;

namespace OpenPDF.Content.Handling
{
    public interface IDictionaryPdfContentFactory
    {
        DictionaryPdfObjectContent Create(IDictionary<string, PdfObjectContent> props);
    }
}