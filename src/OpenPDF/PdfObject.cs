using System;
using System.Collections.Generic;

namespace OpenPDF
{
    public class PdfObject : IEquatable<PdfObject>
    {
        public PdfObject(
            int number,
            int generation,
            string content)
        {
            this.Number = number;
            this.Generation = generation;
            this.Content = content;
        }

        public int Number { get; }
        public int Generation { get; }
        public string Content { get; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PdfObject);
        }

        public virtual bool Equals(PdfObject other)
        {
            return other != null &&
                   this.Number == other.Number &&
                   this.Generation == other.Generation &&
                   this.Content == other.Content;
        }

        public override int GetHashCode()
        {
            int hashCode = -1779235439;
            hashCode = hashCode * -1521134295 + this.Number.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Generation.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Content);
            return hashCode;
        }
    }
}
