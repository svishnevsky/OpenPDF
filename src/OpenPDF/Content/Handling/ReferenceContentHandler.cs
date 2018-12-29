using System.Text.RegularExpressions;

namespace OpenPDF.Content.Handling
{
    public class ReferenceContentHandler : ObjectContentHandler
    {
        internal static readonly Regex ReferenceFormat =
            new Regex(@"^\d+ \d+ R$");

        public ReferenceContentHandler(IObjectContentHandler successor)
            : base(successor)
        {
        }

        protected override bool IsContentSutable(string content)
        {
            return ReferenceFormat.IsMatch(content);
        }

        protected override PdfObjectContent Parse(string content)
        {
            return new ReferencePdfObjectContent(
                PdfReference.Parse(content));
        }
    }
}
