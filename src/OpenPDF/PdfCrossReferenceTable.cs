using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenPDF
{
    public sealed class PdfCrossReferenceTable : IEquatable<PdfCrossReferenceTable>
    {
        private readonly IDictionary<int, PdfCrossReference> references;

        public PdfCrossReferenceTable()
        {
            this.references = new Dictionary<int, PdfCrossReference>();
        }

        public void Add(PdfCrossReference pdfCrossReference)
        {
            this.references.Add(
                pdfCrossReference.Number, pdfCrossReference);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PdfCrossReferenceTable);
        }

        public bool Equals(PdfCrossReferenceTable other)
        {
            return other != null &&
                   this.references.SequenceEqual(other.references);
        }

        public override int GetHashCode()
        {
            return -600369533 + 
                EqualityComparer<IDictionary<int, PdfCrossReference>>
                    .Default.GetHashCode(this.references);
        }

        public PdfCrossReference this[PdfReference reference]
        {
            get
            {
                return this.references[reference.Number];
            }
        }
    }
}
