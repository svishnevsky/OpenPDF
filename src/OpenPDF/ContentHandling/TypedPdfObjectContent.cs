using System;
using System.Collections.Generic;

namespace OpenPDF.ContentHandling
{
    public class TypedPdfObjectContent<T> : PdfObjectContent, IEquatable<TypedPdfObjectContent<T>>
    {
        public TypedPdfObjectContent(T value)
        {
            this.Value = value;
        }

        public T Value { get; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TypedPdfObjectContent<T>);
        }

        public virtual bool Equals(TypedPdfObjectContent<T> other)
        {
            return other != null &&
                   EqualityComparer<T>.Default.Equals(this.Value, other.Value);
        }

        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<T>.Default.GetHashCode(this.Value);
        }
    }
}
