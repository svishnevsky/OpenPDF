namespace OpenPDF.Content.Handling
{
    public class FloatContentHandler : ObjectContentHandler
    {
        public FloatContentHandler(ObjectContentHandler successor)
            : base(successor)
        {
        }

        protected override bool IsContentSutable(string content)
        {
            return float.TryParse(content, out float value);
        }

        protected override PdfObjectContent Parse(string content)
        {
            return new FloatPdfObjectContent(float.Parse(content));
        }
    }
}
