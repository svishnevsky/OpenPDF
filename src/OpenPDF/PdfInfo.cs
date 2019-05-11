using System;
using OpenPDF.Content;

namespace OpenPDF
{
    public sealed class PdfInfo :
        DictionaryPdfObjectContent
    {
        public PdfInfo(DictionaryPdfObjectContent content)
            : base(content.Value)
        {
        }

        public string Author => this.Value<string>("Author");
        public string Title => this.Value<string>("Title");
        public string Producer => this.Value<string>("Producer");
        public DateTime Created => this.Value<DateTime>("CreationDate");
        public DateTime Modified => this.Value<DateTime>("ModDate");
    }
}
