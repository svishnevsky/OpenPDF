using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenPDF.Content
{
    public class DictionaryPdfObjectContent :
        TypedPdfObjectContent<IDictionary<string, PdfObjectContent>>,
        IEquatable<DictionaryPdfObjectContent>
    {
        public DictionaryPdfObjectContent(
            IDictionary<string, PdfObjectContent> value)
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

        public virtual bool Equals(DictionaryPdfObjectContent other)
        {
            return other != null &&
                this.Value.SequenceEqual(other.Value);
        }

        public override int GetHashCode()
        {
            return -600369533 +
                   EqualityComparer<IDictionary<string, PdfObjectContent>>
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
