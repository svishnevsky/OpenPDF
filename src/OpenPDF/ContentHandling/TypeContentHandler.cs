namespace OpenPDF.ContentHandling
{
    public class TypeContentHandler : ObjectContentHandler
    {
        public TypeContentHandler(ObjectContentHandler successor) 
            : base(successor)
        {
        }

        protected override bool IsContentSutable(string content)
        {
            return content?.StartsWith("/") ?? false;
        }

        protected override PdfObjectContent Parse(string content)
        {
            return new TypePdfObjectContent(content.Substring(1));
        }
    }
}
