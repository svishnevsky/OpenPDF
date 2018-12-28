namespace OpenPDF.ContentHandling
{
    public abstract class TypedPdfObjectContent<T> : PdfObjectContent
    {
        public TypedPdfObjectContent(T value)
            : base(value)
        {
        }

        public T Value => (T)this.Content;
    }
}
