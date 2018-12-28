using System;
using System.Linq;
using OpenPDF.ContentHandling;

namespace OpenPDF
{
    public sealed class PdfInfo :
        DictionaryPdfObjectContent, IEquatable<PdfInfo>
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

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PdfInfo);
        }

        public bool Equals(PdfInfo other)
        {
            return other != null &&
                   this.Value.Keys.SequenceEqual(other.Value.Keys) &&
                   this.Value.Values.SequenceEqual(other.Value.Values);
        }

        public override int GetHashCode()
        {
            return 624022166 + base.GetHashCode();
        }
    }
}
