using System;

namespace OpenPDF
{
    public sealed class PdfReference : IEquatable<PdfReference>
    {
        public PdfReference(int number, int generation)
        {
            this.Number = number;
            this.Generation = generation;
        }

        public int Number { get; }
        public int Generation { get; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PdfReference);
        }

        public bool Equals(PdfReference other)
        {
            return other != null &&
                   this.Number == other.Number &&
                   this.Generation == other.Generation;
        }

        public override int GetHashCode()
        {
            int hashCode = 1713845143;
            hashCode = hashCode * -1521134295 + this.Number.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Generation.GetHashCode();
            return hashCode;
        }

        public static PdfReference Parse(string reference)
        {
            string[] parts = reference.Split(' ');
            if (parts.Length != 3 || parts[2] != "R")
            {
                throw new FormatException(ErrorMessages.InvalidReferenceFormat);
            }

            return new PdfReference(int.Parse(parts[0]), int.Parse(parts[1]));
        }
    }
}
