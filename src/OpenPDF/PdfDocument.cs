namespace OpenPDF
{
    public class PdfDocument
    {
        public PdfDocument(string version)
        {
            this.Version = version;
        }

        public string Version { get; }
    }
}
