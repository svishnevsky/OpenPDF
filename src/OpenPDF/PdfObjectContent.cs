using System;
using System.Collections.Generic;

namespace OpenPDF
{
    public class PdfObjectContent : IEquatable<PdfObjectContent>
    {
        public PdfObjectContent(object content)
        {
            this.Content = content;
        }

        public object Content { get; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PdfObjectContent);
        }

        public virtual bool Equals(PdfObjectContent other)
        {
            return other != null &&
                   ((this.Content == null && other.Content == null) ||
                        this.Content?.Equals(other.Content) == true);
        }

        public override int GetHashCode()
        {
            return 1997410482 + EqualityComparer<object>.Default.GetHashCode(this.Content);
        }
    }
}
