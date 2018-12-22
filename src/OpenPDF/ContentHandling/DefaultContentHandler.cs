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
                    var dictionaryHandler = new DictionaryContentHandler(
                            new TypeContentHandler(
                                new ReferenceContentHandler(null)),
                            null);
                    chain = new BoolContentHandler(
                        new StringContentHandler(
                            dictionaryHandler));
                    dictionaryHandler.PropHandler = chain;
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
