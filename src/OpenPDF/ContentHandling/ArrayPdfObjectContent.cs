using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenPDF.ContentHandling
{
    public sealed class ArrayPdfObjectContent : 
        TypedPdfObjectContent<PdfObjectContent[]>,
        IEquatable<ArrayPdfObjectContent>
    {
        public ArrayPdfObjectContent(PdfObjectContent[] value)
            : base(value)
        {
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ArrayPdfObjectContent);
        }

        public override bool Equals(PdfObjectContent other)
        {
            return this.Equals(other as ArrayPdfObjectContent);
        }

        public override bool Equals(
            TypedPdfObjectContent<PdfObjectContent[]> other)
        {
            return this.Equals(other as ArrayPdfObjectContent);
        }

        public bool Equals(ArrayPdfObjectContent other)
        {
            return other != null &&
                this.Value.SequenceEqual(other.Value);
        }

        public override int GetHashCode()
        {
            return -600369533 +
                   EqualityComparer<PdfObjectContent[]>
                       .Default.GetHashCode(this.Value);
        }
    }
}
