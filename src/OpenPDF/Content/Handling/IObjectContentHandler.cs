namespace OpenPDF.Content.Handling
{
    public interface IObjectContentHandler
    {
        PdfObjectContent Handle(string content);
    }
}