namespace OpenPDF.Content.Handling
{
    public class NullContentHandler : ObjectContentHandler
    {
        public NullContentHandler(IObjectContentHandler successor) 
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
