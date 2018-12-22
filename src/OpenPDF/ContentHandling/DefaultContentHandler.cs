namespace OpenPDF.ContentHandling
{
    public class DefaultContentHandler : ObjectContentHandler
    {
        private static ObjectContentHandler chain;

        public DefaultContentHandler() : base(Chain)
        {
        }

        private static ObjectContentHandler Chain
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
                            null);
                    chain = new NullContentHandler(
                        new BoolContentHandler(
                            new StringContentHandler(
                                new TypeContentHandler(
                                    new ReferenceContentHandler(
                                        dictionaryHandler)))));
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
