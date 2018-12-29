using System;
using System.Globalization;

namespace OpenPDF.Content.Handling
{
    public class DateContentHandler : ObjectContentHandler
    {
        public DateContentHandler(IObjectContentHandler successor)
            : base(successor)
        {
        }

        protected override bool IsContentSutable(string content)
        {
            return content.StartsWith(PdfTags.DateStart) &&
                content.EndsWith(PdfTags.DateEnd);
        }

        protected override PdfObjectContent Parse(string content)
        {
            string rawDate = content.Substring(
                PdfTags.DateStart.Length,
                content.Length -
                PdfTags.DateStart.Length -
                PdfTags.DateEnd.Length);
            return new DatePdfObjectContent(
                DateTime.ParseExact(rawDate.Replace('\'', ':'), "yyyyMMddHHmmsszzz", CultureInfo.InvariantCulture));
        }
    }
}
