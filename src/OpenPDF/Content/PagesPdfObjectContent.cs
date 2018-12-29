using System.Collections.Generic;
using System.Linq;

namespace OpenPDF.Content
{
    public class PagesPdfObjectContent : DictionaryPdfObjectContent
    {
        public PagesPdfObjectContent(
            IDictionary<string, PdfObjectContent> value) : base(value)
        {
        }

        public long Count => this.Value<long>("Count");
        public IEnumerable<PdfReference> Kids
        {
            get
            {
                PdfObjectContent[] kidsContent =
                    this.Value<PdfObjectContent[]>("Kids");
                return kidsContent
                    .Select(x => x.Content as PdfReference);
            }
        }
    }
}
