using System;
using System.Collections.Generic;

namespace OpenPDF
{
    public sealed class PdfTrailer : IEquatable<PdfTrailer>
    {
        public PdfTrailer(
            long xrefSeek, 
            int size, 
            PdfReference root, 
            PdfReference info)
        {
            this.XrefSeek = xrefSeek;
            this.Size = size;
            this.Root = root;
            this.Info = info;
        }

        public long XrefSeek { get; }
        public int Size { get; }
        public PdfReference Root { get; }
        public PdfReference Info { get; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PdfTrailer);
        }

        public bool Equals(PdfTrailer other)
        {
            return other != null &&
                   this.XrefSeek == other.XrefSeek &&
                   this.Size == other.Size &&
                   EqualityComparer<PdfReference>.Default.Equals(this.Root, other.Root) &&
                   EqualityComparer<PdfReference>.Default.Equals(this.Info, other.Info);
        }

        public override int GetHashCode()
        {
            int hashCode = 1463874222;
            hashCode = hashCode * -1521134295 + this.XrefSeek.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Size.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<PdfReference>.Default.GetHashCode(this.Root);
            hashCode = hashCode * -1521134295 + EqualityComparer<PdfReference>.Default.GetHashCode(this.Info);
            return hashCode;
        }
    }
}
