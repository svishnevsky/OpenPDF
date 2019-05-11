namespace OpenPDF.Content.Handling
{
    public class DefaultContentHandler : ObjectContentHandler
    {
        private static IObjectContentHandler chain;

        public DefaultContentHandler() : base(Chain)
        {
        }

        private static IObjectContentHandler Chain
        {
            get
            {
                if (chain == null)
                {
                    var arrayHandler = new ArrayContentHandler(
                            new LongContentHandler(
                                new FloatContentHandler(null)),
                            null);
                    var dictionaryHandler = new DictionaryContentHandler(
                            arrayHandler,
                            null,
                            new DictionaryPdfContentFactory());
                    chain = new NullContentHandler(
                        new BoolContentHandler(
                            new DateContentHandler(
                                new StringContentHandler(
                                    new TypeContentHandler(
                                        new ReferenceContentHandler(
                                            new StreamContentHandler(
                                                dictionaryHandler,
                                                dictionaryHandler)))))));
                    dictionaryHandler.PropHandler = chain;
                    arrayHandler.PropHandler = chain;
                }

                return chain;
            }
        }

        protected override bool IsContentSutable(string content)
        {
            return false;
        }

        protected override PdfObjectContent Parse(string content)
        {
            throw new System.NotImplementedException();
        }
    }
}
