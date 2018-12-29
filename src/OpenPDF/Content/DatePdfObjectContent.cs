using System;

namespace OpenPDF.Content
{
    public class DatePdfObjectContent : TypedPdfObjectContent<DateTime>
    {
        public DatePdfObjectContent(DateTime value) : base(value)
        {
        }
    }
}
