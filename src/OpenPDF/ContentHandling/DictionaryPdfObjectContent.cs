using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenPDF.ContentHandling
{
    public class DictionaryPdfObjectContent :
        TypedPdfObjectContent<Dictionary<string, PdfObjectContent>>,
        IEquatable<DictionaryPdfObjectContent>
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

        public override bool Equals(PdfObjectContent other)
        {
            return this.Equals(other as DictionaryPdfObjectContent);
        }

        public bool Equals(DictionaryPdfObjectContent other)
        {
            return other != null &&
                this.Value.SequenceEqual(other.Value);
        }

        public override int GetHashCode()
        {
            return -600369533 +
                   EqualityComparer<Dictionary<string, PdfObjectContent>>
                       .Default.GetHashCode(this.Value);
        }

        public PdfObjectContent this[string key]
        {
            get
            {
                return this.Value.ContainsKey(key)
                    ? this.Value[key]
                    : null;
            }
        }
    }
}
