namespace OpenPDF.ContentHandling
{
    public class StringContentHandler : ObjectContentHandler
    {
        public StringContentHandler(ObjectContentHandler successor)
            : base(successor)
        {
        }

        protected override bool IsContentSutable(string content)
        {
            return content.StartsWith(PdfTags.StringStart) &&
                content.EndsWith(PdfTags.StringEnd);
        }

        protected override PdfObjectContent Parse(string content)
        {
            return new StringPdfObjectContent(content.Substring(
                PdfTags.StringStart.Length,
                content.Length - PdfTags.StringStart.Length - PdfTags.StringEnd.Length));
        }
    }
}
