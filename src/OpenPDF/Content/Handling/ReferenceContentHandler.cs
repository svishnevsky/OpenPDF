using System.Text.RegularExpressions;

namespace OpenPDF.Content.Handling
{
    public class ReferenceContentHandler : ObjectContentHandler
    {
        private static readonly Regex referenceFormat =
            new Regex(@"^\d+ \d+ R$");

        public ReferenceContentHandler(ObjectContentHandler successor)
            : base(successor)
        {
        }

        protected override bool IsContentSutable(string content)
        {
            return referenceFormat.IsMatch(content);
        }

        protected override PdfObjectContent Parse(string content)
        {
            return new ReferencePdfObjectContent(
                PdfReference.Parse(content));
        }
    }
}
