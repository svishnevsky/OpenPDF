using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenPDF.ContentHandling
{
    public class DictionaryContentHandler : ObjectContentHandler
    {
        internal ObjectContentHandler PropHandler { get; set; }

        public DictionaryContentHandler(
            ObjectContentHandler successor,
            ObjectContentHandler propHandler)
            : base(successor)
        {
            this.PropHandler = propHandler;
        }

        protected override bool IsContentSutable(string content)
        {
            return content.StartsWith(PdfTags.DictionaryStart) &&
                content.EndsWith(PdfTags.DictionaryEnd);
        }

        protected override PdfObjectContent Parse(string content)
        {
            content = Unwrap(content);
            var props = new Dictionary<string, PdfObjectContent>();
            while (!string.IsNullOrEmpty(content))
            {
                string prop = content.Substring(1, content.IndexOf(' ') - 1);
                content = content.Substring(prop.Length + 2);
                string[] openBrackets = new[] { "<<", "[", "(" };
                if (!openBrackets.Any(content.StartsWith))
                {
                    int endIndex = content.IndexOf("/", 1);
                    if (endIndex == -1)
                    {
                        endIndex = content.Length;
                    }

                    string value = content.Substring(0, endIndex);
                    content = content.Substring(value.Length);
                    props.Add(prop, this.PropHandler.Handle(value.Trim()));
                    continue;
                }
            }

            return new DictionaryPdfObjectContent(props);
        }

        private static string Unwrap(string content)
        {
            return content
                .Substring(
                    PdfTags.DictionaryStart.Length,
                    content.Length -
                        PdfTags.DictionaryStart.Length -
                        PdfTags.DictionaryEnd.Length)
                .Trim();
        }
    }
}
