using System;

namespace OpenPDF
{
    public class PdfObjectContent : IEquatable<PdfObjectContent>
    {
        public override bool Equals(object obj)
        {
            return this.Equals(obj as PdfObjectContent);
        }

        public virtual bool Equals(PdfObjectContent other)
        {
            return other != null;
        }

        public override int GetHashCode()
        {
            return 1779235439;
        }
    }
}
