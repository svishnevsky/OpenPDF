using System;
using System.Collections.Generic;

namespace OpenPDF
{
    public class PdfDocument : IEquatable<PdfDocument>
    {
        public PdfDocument(string version, PdfInfo info)
        {
            this.Version = version;
            this.Info = info;
        }

        public string Version { get; }
        public PdfInfo Info { get; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PdfDocument);
        }

        public bool Equals(PdfDocument other)
        {
            return other != null &&
                   this.Version == other.Version &&
                   EqualityComparer<PdfInfo>.Default.Equals(this.Info, other.Info);
        }

        public override int GetHashCode()
        {
            int hashCode = 736579316;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Version);
            hashCode = hashCode * -1521134295 + EqualityComparer<PdfInfo>.Default.GetHashCode(this.Info);
            return hashCode;
        }
    }
}
