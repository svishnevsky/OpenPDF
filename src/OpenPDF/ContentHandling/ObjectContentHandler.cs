namespace OpenPDF.ContentHandling
{
    public abstract class ObjectContentHandler
    {
        protected readonly ObjectContentHandler successor;

        protected ObjectContentHandler(ObjectContentHandler successor)
        {
            this.successor = successor;
        }

        public PdfObjectContent Handle(string content)
        {
            if (this.IsContentSutable(content))
            {
                return this.Parse(content);
            }

            return this.successor?.Handle(content);
        }

        protected abstract PdfObjectContent Parse(string content);

        protected abstract bool IsContentSutable(string content);
    }
}
