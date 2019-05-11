namespace OpenPDF.Content.Handling
{
    public class StreamContentHandler : ObjectContentHandler
    {
        private readonly DictionaryContentHandler dictionaryHandler;

        public StreamContentHandler(
            IObjectContentHandler successor,
            DictionaryContentHandler dictionaryContentHandler)
            : base(successor)
        {
            this.dictionaryHandler = dictionaryContentHandler;
        }

        protected override bool IsContentSutable(string content)
        {
            return content.StartsWith(PdfTags.DictionaryStart) && 
                content.EndsWith(PdfTags.StreamEnd);
        }

        protected override PdfObjectContent Parse(string content)
        {
            int streamIndex = content.IndexOf($"\n{PdfTags.StreamStart}");;
            var props = (DictionaryPdfObjectContent)this.dictionaryHandler
                .Handle(content.Substring(0, streamIndex).Trim());
            string streamContent = content.Substring(
                    streamIndex + PdfTags.StreamStart.Length + 1).Trim();
            streamContent = streamContent.Substring(
                0,
                streamContent.Length - PdfTags.StreamEnd.Length)
                .Trim();
            return new StreamPdfObjectContent(new PdfStream(
                props.Value,
                streamContent));
        }
    }
}
