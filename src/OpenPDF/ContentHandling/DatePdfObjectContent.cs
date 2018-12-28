using System;

namespace OpenPDF.ContentHandling
{
    public class DatePdfObjectContent : TypedPdfObjectContent<DateTime>
    {
        public DatePdfObjectContent(DateTime value) : base(value)
        {
        }
    }
}
