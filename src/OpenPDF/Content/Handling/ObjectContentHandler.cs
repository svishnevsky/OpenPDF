namespace OpenPDF.Content.Handling
{
    public abstract class ObjectContentHandler : IObjectContentHandler
    {
        protected readonly IObjectContentHandler successor;

        protected ObjectContentHandler(IObjectContentHandler successor)
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
