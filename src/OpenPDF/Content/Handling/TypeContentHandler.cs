namespace OpenPDF.Content.Handling
{
    public class TypeContentHandler : ObjectContentHandler
    {
        public TypeContentHandler(IObjectContentHandler successor) 
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
