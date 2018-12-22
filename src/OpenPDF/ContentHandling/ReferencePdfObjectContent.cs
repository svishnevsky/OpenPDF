namespace OpenPDF.ContentHandling
{
    public class ReferencePdfObjectContent
        : TypedPdfObjectContent<PdfReference>
    {
        public ReferencePdfObjectContent(PdfReference value) 
            : base(value)
        {
        }
    }
}
