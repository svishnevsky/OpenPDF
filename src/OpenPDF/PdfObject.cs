using System;
using System.Collections.Generic;

namespace OpenPDF
{
    public class PdfObject : IEquatable<PdfObject>
    {
        public PdfObject(
            int number,
            int generation,
            PdfObjectContent content)
        {
            this.Number = number;
            this.Generation = generation;
            this.Content = content;
        }

        public int Number { get; }
        public int Generation { get; }
        public PdfObjectContent Content { get; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PdfObject);
        }

        public bool Equals(PdfObject other)
        {
            return other != null &&
                   this.Number == other.Number &&
                   this.Generation == other.Generation &&
                   EqualityComparer<PdfObjectContent>.Default.Equals(this.Content, other.Content);
        }

        public override int GetHashCode()
        {
            int hashCode = -1779235439;
            hashCode = hashCode * -1521134295 + this.Number.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Generation.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<PdfObjectContent>.Default.GetHashCode(this.Content);
            return hashCode;
        }
    }
}
