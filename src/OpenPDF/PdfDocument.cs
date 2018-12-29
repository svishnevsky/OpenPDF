using System;
using System.Collections.Generic;
using System.Linq;
using OpenPDF.Content;

namespace OpenPDF
{
    public class PdfDocument : IEquatable<PdfDocument>
    {
        public PdfDocument(
            string version,
            PdfInfo info,
            IReadOnlyCollection<PagePdfObjectContent> pages)
        {
            this.Version = version;
            this.Info = info;
            this.Pages = pages;
        }

        public string Version { get; }
        public PdfInfo Info { get; }
        public IReadOnlyCollection<PagePdfObjectContent> Pages { get; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PdfDocument);
        }

        public bool Equals(PdfDocument other)
        {
            return other != null &&
                   this.Version == other.Version &&
                   EqualityComparer<PdfInfo>.Default.Equals(this.Info, other.Info) &&
                   this.Pages.SequenceEqual(other.Pages);
        }

        public override int GetHashCode()
        {
            int hashCode = 736579316;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Version);
            hashCode = hashCode * -1521134295 + EqualityComparer<PdfInfo>.Default.GetHashCode(this.Info);
            hashCode = hashCode * -1521134295 + EqualityComparer<IReadOnlyCollection<PagePdfObjectContent>>.Default.GetHashCode(this.Pages);
            return hashCode;
        }
    }
}
