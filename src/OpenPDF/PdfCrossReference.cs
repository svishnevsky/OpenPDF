using System;

namespace OpenPDF
{
    public sealed class PdfCrossReference : IEquatable<PdfCrossReference>
    {
        public PdfCrossReference(
            int number,
            long seek,
            int generation,
            bool inUse)
        {
            this.Number = number;
            this.Seek = seek;
            this.Generation = generation;
            this.InUse = inUse;
        }

        public int Number { get; }
        public long Seek { get; }
        public int Generation { get; }
        public bool InUse { get; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PdfCrossReference);
        }

        public bool Equals(PdfCrossReference other)
        {
            return other != null &&
                   this.Number == other.Number &&
                   this.Seek == other.Seek &&
                   this.Generation == other.Generation &&
                   this.InUse == other.InUse;
        }

        public override int GetHashCode()
        {
            int hashCode = -1515883127;
            hashCode = hashCode * -1521134295 + this.Number.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Seek.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Generation.GetHashCode();
            hashCode = hashCode * -1521134295 + this.InUse.GetHashCode();
            return hashCode;
        }
    }
}
