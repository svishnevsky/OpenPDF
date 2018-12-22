namespace OpenPDF.ContentHandling
{
    public class BoolContentHandler : ObjectContentHandler
    {
        public BoolContentHandler(ObjectContentHandler successor) 
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
