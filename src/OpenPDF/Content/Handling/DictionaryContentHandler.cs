using System.Collections.Generic;

namespace OpenPDF.Content.Handling
{
    public class DictionaryContentHandler : ComplexContentBaseHandler
    {
        private readonly IDictionaryPdfContentFactory factory;

        public DictionaryContentHandler(
            IObjectContentHandler successor,
            IObjectContentHandler propHandler,
            IDictionaryPdfContentFactory factory)
            : base(
                  successor,
                  propHandler,
                  PdfTags.DictionaryStart,
                  PdfTags.DictionaryEnd,
                  "/")
        {
            this.factory = factory;
        }

        protected override PdfObjectContent Parse(string content)
        {
            content = this.Unwrap(content);
            var props = new Dictionary<string, PdfObjectContent>();
            while (!string.IsNullOrEmpty(content))
            {
                string prop = content.Substring(1, content.IndexOf(' ') - 1);
                content = content.Substring(prop.Length + 2);
                string value = this.GetValue(content);
                content = content.Substring(value.Length).TrimStart();
                props.Add(prop, this.PropHandler.Handle(value.Trim()));
            }

            return this.factory.Create(props);
        }
    }
}
