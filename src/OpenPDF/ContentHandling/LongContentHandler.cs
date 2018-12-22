using System.Linq;

namespace OpenPDF.ContentHandling
{
    public class LongContentHandler : ObjectContentHandler
    {
        public LongContentHandler(ObjectContentHandler successor)
            : base(successor)
        {
        }

        protected override bool IsContentSutable(string content)
        {
            return content.All(char.IsDigit);
        }

        protected override PdfObjectContent Parse(string content)
        {
            return new LongPdfObjectContent(long.Parse(content));
        }
    }
}
