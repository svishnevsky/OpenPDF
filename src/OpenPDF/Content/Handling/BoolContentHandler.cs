namespace OpenPDF.Content.Handling
{
    public class BoolContentHandler : ObjectContentHandler
    {
        public BoolContentHandler(IObjectContentHandler successor) 
            : base(successor)
        {
        }

        protected override bool IsContentSutable(string content)
        {
            return "true".Equals(content) || "false".Equals(content);
        }

        protected override PdfObjectContent Parse(string content)
        {
            return new BoolPdfObjectContent(bool.Parse(content));
        }
    }
}
