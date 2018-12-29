namespace OpenPDF.Content.Handling
{
    public class NullContentHandler : ObjectContentHandler
    {
        public NullContentHandler(ObjectContentHandler successor) 
            : base(successor)
        {
        }

        protected override bool IsContentSutable(string content)
        {
            return "null".Equals(content);
        }

        protected override PdfObjectContent Parse(string content)
        {
            return new NullPdfObjectContent();
        }
    }
}
