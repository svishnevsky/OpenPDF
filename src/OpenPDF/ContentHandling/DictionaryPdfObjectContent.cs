using System.Collections.Generic;
using System.Linq;

namespace OpenPDF.ContentHandling
{
    public sealed class DictionaryPdfObjectContent :
        TypedPdfObjectContent<Dictionary<string, PdfObjectContent>>
    {
        public DictionaryPdfObjectContent(
            Dictionary<string, PdfObjectContent> value) 
            : base(value)
        {
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DictionaryPdfObjectContent);
        }

        public bool Equals(DictionaryPdfObjectContent other)
        {
            return other != null &&
                this.Value.SequenceEqual(other.Value);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
